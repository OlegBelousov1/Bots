using Telegram.Bot.Types.ReplyMarkups;

namespace CardsBot.Services.Interfaces
{
    public interface IKeyboardManager
    {
        public ReplyKeyboardMarkup GetMain();
    }
}
