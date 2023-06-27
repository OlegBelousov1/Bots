using CardsBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CardsBot.BOT.Commands
{
    public class CreditCardsCommand : Command
    {
        public CreditCardsCommand(IKeyboardManager keyboardManager, IMessageManager messageManager)
            : base(keyboardManager, messageManager)
        {
        }

        public override string Name => "Кредитные карты";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Message.From.Id, 
                await _messageManager.GetMessageTextAsync(Models.MessageType.Credit), parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                replyMarkup: _keyboardManager.GetMain());
        }
    }
}
