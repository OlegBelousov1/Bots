using InstaDirect.Models;

namespace InstaDirect.Services.Interfaces
{
    public interface IUserManager
    {
        Task<BotUser> GetUserByTIdAsync(long tid);
        Task<bool> IsUserBannedAsync(long tid);
        Task<IEnumerable<BotUser>> GetBotUsersAsync();
        Task<bool> EditUserAsync(BotUser user);
        Task AddUserAsync(BotUser user);
    }
}
