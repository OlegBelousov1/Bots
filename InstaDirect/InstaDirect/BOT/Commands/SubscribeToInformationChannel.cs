using InstaDirect.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstaDirect.BOT.Commands
{
    public class SubscribeToInformationChannel : Command
    {
        private readonly ISubscribeManager _subscribeManager;

        public SubscribeToInformationChannel(ISubscribeManager subscribeManager)
        {
            _subscribeManager = subscribeManager;
        }

        public override string Name => "SubscribeToInformationChannel";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id, null);
            var url = await _subscribeManager.GetInformationChannelLinkAsync();
            var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Подписаться", url)
                }
            });
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, "Чтобы подписаться на информационный канал, нажмите кнопку ниже", replyMarkup: keyboard);
        }
    }
}
