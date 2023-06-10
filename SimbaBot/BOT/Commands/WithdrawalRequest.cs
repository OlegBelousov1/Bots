using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimbaBot.BOT.Commands
{
    public class WithdrawalRequest : Command
    {
        public WithdrawalRequest(IUserManager userManager, Bot bot, IRepository<BotUser> repository)
            : base(userManager, bot, repository)
        {
        }

        public override string Name => "WithdrawalRequest";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await _userManager.SetStatusToUser(message.CallbackQuery.From.Id, UserStatus.ReadyOrder);
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, "Укажите, куда вывести деньги\nПример заявки: Киви +79375790401");
        }
    }
}
