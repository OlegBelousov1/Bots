using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Interfaces;

namespace SimbaBot.Services.Implementations
{
    public class UserManager : IUserManager
    {
        private readonly IRepository<BotUser> _repository;

        public UserManager(IRepository<BotUser> repository)
        {
            _repository = repository;
        }

        public async Task<bool> EditUserAsync(BotUser user)
        {
            var botUser = await _repository.FirstOrDefaultAsync(i => i.TId == user.TId);
            if (botUser == null) 
                return false;
            _repository.Update(user);
            await _repository.SaveChangesAsync();
            return true;
        }

        public Task<BotUser> GetBotUserAsync(long tid)
        {
            return _repository.FirstOrDefaultAsync(i => i.TId == tid);
        }

        public async Task<IEnumerable<BotUser>> GetBotUsersAsync()
        {
            return await _repository.ToListAsync();
        }

        public async Task<decimal> GetUserBalanceAsync(long tid)
        {
            var user = await _repository.FirstOrDefaultAsync(i => i.TId == tid);
            return user.Balance;
        }

        public async Task SetStatusToUser(long tid, UserStatus status)
        {
            var user = await GetBotUserAsync(tid);
            if (user != null)
            {
                user.Status = status;
                await _repository.SaveChangesAsync();
            }
        }
    }
}
