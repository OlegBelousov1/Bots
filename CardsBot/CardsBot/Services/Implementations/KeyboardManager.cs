using CardsBot.Services.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace CardsBot.Services.Implementations
{
    public class KeyboardManager : IKeyboardManager
    {
        public ReplyKeyboardMarkup GetMain()
        {
            var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new []
                    {
                        new KeyboardButton("Кредитные карты"), new KeyboardButton("Дебетовые карты")
                    },
                });
            keyboard.ResizeKeyboard = true;
            return keyboard;
        }
    }
}
