using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimbaBot.BOT.Commands
{
    public class Subscribe : Command
    {
        public Subscribe(IUserManager userManager, Bot bot, IRepository<BotUser> repository) 
            : base(userManager, bot, repository)
        {
        }

        public override string Name => "Subscribe";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("• 1 месяц - 100 рублей 🔥", "Bought,1")
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("• 6 месяцев - 300 рублей 🔥🔥", "Bought,2")
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("• Год - 500 рублей 🔥🔥🔥", "Bought,3")
                },
            });
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, "Выберите период",
                Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: keyboard);
        }
    }
}
