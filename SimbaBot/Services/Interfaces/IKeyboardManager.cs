using Telegram.Bot.Types.ReplyMarkups;

namespace SimbaBot.Services.Interfaces
{
    public interface IKeyboardManager
    {
        public InlineKeyboardMarkup GetMainKeyboard();
    }
}
