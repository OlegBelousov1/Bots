using Telegram.Bot;
using Telegram.Bot.Types;

namespace InstaDirect.BOT.Commands
{
    public class Support : Command
    {
        public override string Name => "Support";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id, null);
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, "Напишите в лс @instadirect_acc");
        }
    }
}
