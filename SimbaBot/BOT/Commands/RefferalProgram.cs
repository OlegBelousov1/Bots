using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimbaBot.BOT.Commands
{
    public class RefferalProgram : Command
    {
        public RefferalProgram(IUserManager userManager, Bot bot, IRepository<BotUser> repository)
            : base(userManager, bot, repository)
        {
        }

        public override string Name => "RefferalProgram";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Моя реферальная ссылка","RefferLink")
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("О реферальной программе", "https://i-leon.ru/tools/time")
                }
            });
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, "Реферальная программа", replyMarkup: keyboard);
        }
    }
}
