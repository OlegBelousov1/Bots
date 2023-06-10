using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimbaBot.BOT.Commands
{
    public class MyBalance : Command
    {
        public MyBalance(IUserManager userManager, Bot bot, IRepository<BotUser> repository) : base(userManager, bot, repository)
        {
        }

        public override string Name => "MyBalance";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            var balance = await _userManager.GetUserBalanceAsync(message.CallbackQuery.From.Id);
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, $"Ваш баланс в рублях: {balance}");
        }
    }
}
