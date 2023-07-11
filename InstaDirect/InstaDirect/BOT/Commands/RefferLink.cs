using InstaDirect.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace InstaDirect.BOT.Commands
{
    public class RefferLink : Command
    {
        private readonly IRefferalManager _refferalManager;

        public RefferLink(IRefferalManager refferalManager)
        {
            _refferalManager = refferalManager;
        }

        public override string Name => "RefferLink";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id, null);
            var refferals = await _refferalManager.GetGoodRefferalsAsync(message.CallbackQuery.From.Id);
            var result = $"Ваша реферальная ссылка: https://t.me/olezzzhatest_bot?start={message.CallbackQuery.From.Id}\n" +
                $"Кол-во приглашенных за сегодня: {refferals.Where(t => t.JoinTime > DateTime.Now.AddDays(-1)).Count()} \n" +
                $"Кол-во приглашенных за неделю: {refferals.Where(t=>t.JoinTime > DateTime.Now.AddDays(-7)).Count()}\n" +
                $"Кол-во приглашенных за месяц: {refferals.Where(t => t.JoinTime > DateTime.Now.AddDays(-30)).Count()}";
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, result);
        }
    }
}
