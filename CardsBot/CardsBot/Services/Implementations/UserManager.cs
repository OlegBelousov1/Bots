using CardsBot.Models;
using CardsBot.Repository;
using CardsBot.Services.Interfaces;

namespace CardsBot.Services.Implementations
{
    public class UserManager : IUserManager
    {
        private readonly IRepository<BotUser> _repository;

        public UserManager(IRepository<BotUser> repository)
        {
            _repository = repository;
        }

        public async Task AddUserAsync(BotUser user)
        {
            var checkUser = await GetUserByTIDAsync(user.TId);
            if (checkUser == null)
            {
                await _repository.AddAsync(user);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<bool> EditUserAsync(BotUser user)
        {
            var botUser = await GetUserByTIDAsync(user.TId);
            if (botUser == null)
                return false;
            _repository.Update(botUser);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BotUser>> GetAllUsersAsync()
        {
            return await _repository.ToListAsync();
        }

        public async Task<int> GetCountUsersWhoBannedBotAsync()
        {
            var users = await _repository.ToListAsync(i => i.BotIsBanned);
            return users.Count;
        }

        public async Task<BotUser> GetUserByTIDAsync(long tid)
        {
            return await _repository.FirstOrDefaultAsync(i => i.TId == tid);
        }

        public async Task<bool> IsUserBannedAsync(long tid)
        {
            var user = await GetUserByTIDAsync(tid);
            return user.Banned;
        }

        public async Task SetStatusToUserAsync(long tid, string status)
        {
            var user = await GetUserByTIDAsync(tid);
            if (user != null)
            {
                user.Status = status;
                await _repository.SaveChangesAsync();
            }
        }

        public async Task SetTrueToBotIsBannedAsync(int id)
        {
            var user = await _repository.FirstOrDefaultAsync(i => i.Id == id);
            user.BotIsBanned = true;
            await _repository.SaveChangesAsync();   
        }
    }
}
