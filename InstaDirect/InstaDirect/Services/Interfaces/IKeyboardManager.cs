using Telegram.Bot.Types.ReplyMarkups;

namespace InstaDirect.Services.Interfaces
{
    public interface IKeyboardManager
    {
        public Task<InlineKeyboardMarkup> GetMainKeyboardAsync();
    }
}
