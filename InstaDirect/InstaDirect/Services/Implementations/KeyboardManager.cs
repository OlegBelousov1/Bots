using InstaDirect.Services.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace InstaDirect.Services.Implementations
{
    public class KeyboardManager : IKeyboardManager
    {
        private readonly ISubscribeManager _subscribeManager;

        public KeyboardManager(ISubscribeManager subscribeManager)
        {
            _subscribeManager = subscribeManager;
        }

        public async Task<InlineKeyboardMarkup> GetMainKeyboardAsync()
        {
            return new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                                          new InlineKeyboardButton[]
                                         {
                                            InlineKeyboardButton.WithUrl("Канал информации", await _subscribeManager.GetInformationChannelLinkAsync()),
                                         },
                                         new InlineKeyboardButton[]
                                         {
                                            InlineKeyboardButton.WithUrl("Чат InstaDirect Message", await _subscribeManager.GetLiteChannelLinkAsync()),
                                         },
                                         new InlineKeyboardButton[]
                                         {
                                            InlineKeyboardButton.WithUrl("Чат InstaWiq Bot", await _subscribeManager.GetWiqChannelLinkAsync()),
                                         },
                                         new InlineKeyboardButton[]
                                         {
                                            InlineKeyboardButton.WithCallbackData("Купить регистратор аккаунтов InstaDirect PRO", "BuyRegPro"),
                                         },
                                          new InlineKeyboardButton[]
                                         {
                                            InlineKeyboardButton.WithCallbackData("Связь с администрацией", "Support"),
                                         },
                                          new InlineKeyboardButton[]
                                          {
                                              InlineKeyboardButton.WithCallbackData("Получить аккаунт", "GetAccount"),
                                          },
                                          new InlineKeyboardButton[]
                                          {
                                              InlineKeyboardButton.WithCallbackData("Реферальная ссылка", "RefferLink")
                                          }

            });
        }
    }
}
