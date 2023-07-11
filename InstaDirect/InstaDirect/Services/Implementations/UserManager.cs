using InstaDirect.Models;
using InstaDirect.Repository;
using InstaDirect.Services.Interfaces;

namespace InstaDirect.Services.Implementations
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
            var checkUser = await _repository.FirstOrDefaultAsync(i => i.TId == user.TId);
            if (checkUser == null)
            {
                await _repository.AddAsync(user);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<bool> EditUserAsync(BotUser user)
        {
            var botUser = await _repository.FirstOrDefaultAsync(i => i.TId == user.TId);
            if (botUser == null)
                return false;
            botUser.Banned = user.Banned;
            _repository.Update(botUser);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BotUser>> GetBotUsersAsync()
        {
            return await _repository.ToListAsync();
        }

        public async Task<BotUser> GetUserByTIdAsync(long tid)
        {
            return await _repository.FirstOrDefaultAsync(i => i.TId == tid);
        }

        public async Task<bool> IsUserBannedAsync(long tid)
        {
            var user = await _repository.FirstOrDefaultAsync(i => i.TId == tid);
            return user.Banned;
        }
    }
}
