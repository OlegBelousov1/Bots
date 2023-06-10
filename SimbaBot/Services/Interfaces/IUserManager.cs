using SimbaBot.Models;

namespace SimbaBot.Services.Interfaces
{
    public interface IUserManager
    {
        public Task<IEnumerable<BotUser>> GetBotUsersAsync();
        public Task<BotUser> GetBotUserAsync(long tid);
        public Task<bool> EditUserAsync(BotUser user);
        public Task<decimal> GetUserBalanceAsync(long tid);
        public Task SetStatusToUser(long tid, UserStatus status);
    }
}
