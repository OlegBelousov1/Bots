using Azure.Core.Pipeline;
using SimbaBot.Services.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimbaBot.Services.Implementations
{
    public class KeyboardManager : IKeyboardManager
    {
        public InlineKeyboardMarkup GetMainKeyboard()
        {
            var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Оформить подписку","Subscribe"),
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Мой баланс", "MyBalance"),
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Вывод баланса", "WithdrawalOfBalance"),
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Обучение заработку (Чат команды)", "https://i-leon.ru/tools/time"),
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Реферальная программа", "https://i-leon.ru/tools/time"),
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Связь с поддержкой", "https://i-leon.ru/tools/time"),
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithUrl("Реклама на канале/в боте.", "https://i-leon.ru/tools/time"),
                },
            });
            return keyboard;
        }
    }
}
