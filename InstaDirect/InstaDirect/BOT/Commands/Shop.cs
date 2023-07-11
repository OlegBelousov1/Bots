using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstaDirect.BOT.Commands
{
    public class Shop : Command
    {
        public override string Name => "Shop";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id, null);
            var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Перейти в магазин", "https://instadirect-aks.deer.is/")
                }
            });
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, "Чтобы перейти в магазин, нажмите кнопку ниже", replyMarkup: keyboard);
        }
    }
}
