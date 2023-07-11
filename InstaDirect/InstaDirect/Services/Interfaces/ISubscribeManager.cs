using Telegram.Bot;
using Telegram.Bot.Types;

namespace InstaDirect.Services.Interfaces
{
    public interface ISubscribeManager
    {
        public Task<bool> IsUserInGroupAndChannelAsync(long tid);
        public Task SendNotIfNotUserInGroupAsync(long tid);
        public Task<string> GetInformationChannelLinkAsync();
        public Task<string> GetLiteChannelLinkAsync();
        public Task<string> GetWiqChannelLinkAsync();
    }
}
