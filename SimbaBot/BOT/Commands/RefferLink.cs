using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimbaBot.BOT.Commands
{
    public class RefferLink : Command
    {
        public RefferLink(IUserManager userManager, Bot bot, IRepository<BotUser> repository)
            : base(userManager, bot, repository)
        {
        }

        public override string Name => "RefferLink";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            var mess = $"Ваша реферальная ссылка: https://t.me/olezzzhatest_bot?start={message.CallbackQuery.From.Id}";
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, mess);
        }
    }
}
