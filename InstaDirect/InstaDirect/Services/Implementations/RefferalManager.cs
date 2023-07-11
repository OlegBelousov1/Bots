using InstaDirect.BOT;
using InstaDirect.Models;
using InstaDirect.Models.ViewModels;
using InstaDirect.Repository;
using InstaDirect.Services.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace InstaDirect.Services.Implementations
{
    public class RefferalManager : IRefferalManager
    {
        private readonly IRepository<RefferalsInfo> _repository;
        private readonly TelegramBotClient _client;
        private readonly ChannelsIds _channelIds;
        private readonly IUserManager _userManager;

        public RefferalManager(IRepository<RefferalsInfo> repository, Bot bot, IOptions<ChannelsIds> options,
            IUserManager userManager)
        {
            _repository = repository;
            _client = bot.Get();
            _channelIds = options.Value;
            _userManager = userManager;
        }

        public async Task AddRefferalAsync(RefferalsInfo refferal)
        {
            await _repository.AddAsync(refferal);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> CheckForАvailabilityRefferalAsync(long refferalTId, long inviterTId)
        {
            var check = await _repository.FirstOrDefaultAsync(i => i.RefferalTId == refferalTId && i.InviterTId == inviterTId);
            return check == null;
        }

        public async Task DeleteAllRefferalsAsync()
        {
            var refs = await _repository.ToListAsync();
            _repository.RemoveRange(refs);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteRefferalAsync(int id)
        {
            var refferal = await _repository.FirstOrDefaultAsync(i => i.Id == id);
            _repository.Remove(refferal);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<FullRefferalInfo>> GetFullRefferalsInfoAsync(long tid)
        {
            var refferals = await _repository.ToListAsync(i => i.InviterTId == tid);
            var result = refferals.Select(i => new FullRefferalInfo() { Id = i.Id, TId = i.RefferalTId, Date = i.JoinTime }).ToList();
            foreach (var refferal in result)
            {
                refferal.Name = _userManager.GetUserByTIdAsync(refferal.TId).Result.Name;
            }
            return result;
        }

        public async Task<IEnumerable<RefferalsInfo>> GetGoodRefferalsAsync(long tid = 0)
        {
            var refferals = new List<RefferalsInfo>();
            if (tid != 0)
                refferals = await _repository.ToListAsync(i => i.InviterTId == tid);
            else
                refferals = await _repository.ToListAsync();

            foreach (var refferal in refferals)
            {
                try
                {
                    var chatMember = await _client.GetChatMemberAsync(_channelIds.LiteChannel, refferal.RefferalTId);
                    if (chatMember.Status == ChatMemberStatus.Left && chatMember.Status == ChatMemberStatus.Kicked)
                    {
                        refferals.Remove(refferal);
                        _repository.Remove(refferal);
                        await _repository.SaveChangesAsync();
                    }
                }
                catch (Exception) { }
            }

            return refferals;
        }

        public async Task<IEnumerable<TopRefferals>> GetTopRefferalsAsync(int days)
        {
            var goodRefs = await GetGoodRefferalsAsync();
            var topRefs = goodRefs.Where(i => i.JoinTime > DateTime.Now.AddDays(-1 * days))
                .GroupBy(i => i.InviterTId)
                .Select(i => new TopRefferals() { TId = i.Key, RefferalsCount = i.Count() })
                .OrderByDescending(i => i.RefferalsCount)
                .Take(5)
                .ToList();
            foreach (var refferal in topRefs)
            {
                refferal.Name = _userManager.GetUserByTIdAsync(refferal.TId).Result.Name;
            }
            return topRefs;
        }
    }
}
