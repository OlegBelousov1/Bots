using CardsBot.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CardsBot.BOT.Commands
{
    public abstract class Command
    {
        public readonly IKeyboardManager _keyboardManager;
        public readonly IMessageManager _messageManager;
        public Command(IKeyboardManager keyboardManager, IMessageManager messageManager)
        {
            _keyboardManager = keyboardManager;
            _messageManager = messageManager;
        }

        public abstract string Name { get;}

        public abstract Task ExecuteAsync(Update message, TelegramBotClient client);

        public bool Contains(string message)
        {
            return message.ToLower().Contains(Name.ToLower());
        }
    }
}
