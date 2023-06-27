using CardsBot.Models;
using CardsBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CardsBot.BOT.Commands
{
    public class HelloCommand : Command
    {
        private readonly IUserManager _userManager;

        public HelloCommand(IUserManager userManager, IKeyboardManager keyboardManager, IMessageManager messageManager)
            : base(keyboardManager, messageManager)
        {
            _userManager = userManager;
        }

        public override string Name => "/start";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            var user = await _userManager.GetUserByTIDAsync(message.Message.From.Id);

            user ??= new BotUser
                {
                    FirstDate = DateTime.UtcNow,
                    Name = message.Message.From.Username,
                    TId = message.Message.From.Id
                };

            await _userManager.AddUserAsync(user);
            var keyboard = _keyboardManager.GetMain();
            await client.SendTextMessageAsync(user.TId, await _messageManager.GetMessageTextAsync(MessageType.Hello),
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: keyboard);
        }
    }
}
