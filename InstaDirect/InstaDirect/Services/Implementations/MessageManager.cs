using InstaDirect.BOT;
using InstaDirect.Services.Interfaces;
using Telegram.Bot;

namespace InstaDirect.Services.Implementations
{
    public class MessageManager : IMessageManager
    {
        private readonly IUserManager _userManager;
        private readonly TelegramBotClient _client;

        public MessageManager(IUserManager userManager, Bot bot)
        {
            _userManager = userManager;
            _client = bot.Get();
        }

        public async Task<int> SendMessagesAsync(string message)
        {
            var counter = 0;
            var users = await _userManager.GetBotUsersAsync();
            foreach (var user in users) 
            {
                await _client.SendTextMessageAsync(user.TId, message);
                counter++;
            }
            return counter;
        }
    }
}
