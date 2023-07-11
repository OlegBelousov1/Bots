using InstaDirect.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace InstaDirect.BOT.Commands
{
    public class GetAccount : Command
    {
        private readonly IAccountManager _accountManager;

        public GetAccount(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public override string Name => "GetAccount";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id, null);

            var tid = message.CallbackQuery.From.Id;
            if (await _accountManager.CheckForRecievedAccountTodayAsync(tid))
            {
                await client.SendTextMessageAsync(tid, "На сегодня Вы уже получили Вашу лицензию");
                return;
            }

            var accountString = await _accountManager.GetAccountDataAsync(tid);
            if (accountString == null)
            {
                await client.SendTextMessageAsync(tid, "Аккаунтов больше нет, дождитесь пополнения");
                return;
            }
            await client.SendTextMessageAsync(tid, $"Ваш логин: {accountString.Split(':')[0]}\nВаш пароль: {accountString.Split(':')[1]}");
        }
    }
}
