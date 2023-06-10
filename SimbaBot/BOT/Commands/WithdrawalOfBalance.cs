using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimbaBot.BOT.Commands
{
    public class WithdrawalOfBalance : Command
    {
        public WithdrawalOfBalance(IUserManager userManager, Bot bot, IRepository<BotUser> repository)
            : base(userManager, bot, repository)
        {
        }

        public override string Name => "WithdrawalOfBalance";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            var tid = message.CallbackQuery.From.Id;
            var keyboard = new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Отправить заявку на вывод", "WithdrawalRequest"));
            var balance = await _userManager.GetUserBalanceAsync(tid);
            if (balance < 500)
                await client.SendTextMessageAsync(tid, "Сумма на вашем балансе меньше минимальной для вывода (500 руб.)");
            else
                await client.SendTextMessageAsync(tid, $"Ваш баланс: {balance}, вы можете отправить заявку на вывод",
                    replyMarkup: keyboard);
        }
    }
}
