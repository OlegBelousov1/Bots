using CardsBot.Models;

namespace CardsBot.Services.Interfaces
{
    public interface IUserManager
    {
        Task<BotUser> GetUserByTIDAsync(long tid);
        Task<bool> IsUserBannedAsync(long tid);
        Task<IEnumerable<BotUser>> GetAllUsersAsync();
        Task<bool> EditUserAsync(BotUser user);
        Task SetStatusToUserAsync(long tid, string status);
        Task AddUserAsync(BotUser user);
        Task<int> GetCountUsersWhoBannedBotAsync();
        Task SetTrueToBotIsBannedAsync(int id);
    }
}
