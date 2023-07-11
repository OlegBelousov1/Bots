using InstaDirect.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstaDirect.BOT.Commands
{
    public class SubscribeToLiteChannel : Command
    {
        private readonly ISubscribeManager _subscribeManager;

        public SubscribeToLiteChannel(ISubscribeManager subscribeManager)
        {
            _subscribeManager = subscribeManager;
        }

        public override string Name => "SubscribeToLiteChannel";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id, null);
            var url = await _subscribeManager.GetLiteChannelLinkAsync();
            var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Подписаться", url)
                }
            });
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, "Чтобы подписаться на Lite чат регистратора, нажмите кнопку ниже", replyMarkup: keyboard);
        }
    }
}
