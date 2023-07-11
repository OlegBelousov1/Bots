using Telegram.Bot;
using Telegram.Bot.Types;

namespace InstaDirect.BOT.Commands
{
    public class BuyRegPro : Command
    {
        public override string Name => "BuyRegPro";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            await client.AnswerCallbackQueryAsync(message.CallbackQuery.Id, null);
            await client.SendTextMessageAsync(message.CallbackQuery.From.Id, "PRO лицензия регистратора InstaDirect PRO становится доступной для каждого нашего клиента 🦁\n\nПлюсы брать месячную лицензию 🐬\n\n• Регистратор InstaDirect Reger актуален уже более 3х лет ✅ (Не один из регистраторов на рынке не проработал и не обновлялся так долго, обычно они пропадают на пару месяцев и снова появляются, наш же актуален уже более 3х лет)• Постоянные обновления и лучший пробив на API и обновленный WEB ✅• Нашим регистратором регистрируется в месяц более 1млн аккаунтов ✅• Доступ в чат и доступ к проксям для регистрации ✅\n\nЦена PRO лицензии - 10.000 рублей в месяц. (+20 % комиссии с аккаунтов, за постоянные обновления и поддержку) 🔑\n\n\n\nКупить - @instadirect_acc\n\nВсе наши софты делаются для вас, и для вашей комфортной работы! 🚀Приятной работы с InstaDirect 🦁");
        }
    }
}
