using CardsBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CardsBot.BOT.Commands
{
    public class DebetCardsCommand : Command
    {
        public DebetCardsCommand(IKeyboardManager keyboardManager, IMessageManager messageManager)
            : base(keyboardManager, messageManager)
        {
        }

        public override string Name => "Дебетовые карты";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Message.From.Id,
                await _messageManager.GetMessageTextAsync(Models.MessageType.Debet), parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                replyMarkup: _keyboardManager.GetMain());
        }
    }
}
