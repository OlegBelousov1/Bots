using CardsBot.BOT;
using CardsBot.Models;
using CardsBot.Repository;
using CardsBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CardsBot.Services.Implementations
{
    public class MessageManager : IMessageManager
    {
        private readonly IRepository<Models.Message> _repository;
        private readonly IUserManager _userManager;
        private readonly Bot _bot;
        public MessageManager(IRepository<Models.Message> repository, IUserManager userManager, Bot bot)
        {
            _repository = repository;
            _userManager = userManager;
            _bot = bot;
        }

        public async Task EditMessageAsync(string message, MessageType type)
        {
            var oldMessage = await _repository.FirstOrDefaultAsync(i => i.MessageType == type);
            if (oldMessage == null)
            {
                oldMessage = new Models.Message { MessageType = type, Text = message };
                await _repository.AddAsync(oldMessage);
            }
            else
                oldMessage.Text = message;
            await _repository.SaveChangesAsync();
        }

        public async Task<Models.Message> GetMessageAsync(MessageType type)
        {
            var message = await _repository.FirstOrDefaultAsync(i => i.MessageType == type);
            if (message != null)
                return message;
            else
                return new Models.Message { Text = "Сообщение", MessageType = type };
        }

        public async Task<string> GetMessageTextAsync(MessageType type)
        {
            var message = await _repository.FirstOrDefaultAsync(i => i.MessageType == type);
            if (message != null)
                return message.Text;
            else
                return "Собрали для вас самые выгодные и актуальные предложения на данный момент:\n\nКредитная карта альфа банка «365 дней без%»:\n— целый год без %\n— выпуск и обслуживание бесплатно\n— кредитный лимит до 500 000₽\nРешение по одобрению около 5 минут — оформить карту.\n\nКредитная карта тинькофф банка «тинькофф платинум»:\n— 55 дней без %\n— кредитный лимит до 700 000₽\n— переводы на карты других банков до 50 000 рублей вмесяц бесплатно\n— бесплатное обслуживание идоставка в любое время иместо\nРешение по одобрению около 2 минут — оформить карту.\n\nКредитная карта от Ренессанс Кредит «Разумная»:\n— 145 дней без % на всё (покупки, снятие наличных, переводы себе на карту и близким)\n— снятие и переводы безкомиссии\n— бесплатное обслуживание и доставка в любое время и место\n— кредитный лимит до 600 000₽\nРешение по одобрению за 1 минуту — оформить карту.\n\nКредитная карта от Газпромбанка «180 дней без%»:\n— целых полгода без %\n— бесплатное обслуживание и доставка в любое время и место\n— кредитный лимит до 1 000 000₽\nРешение по одобрению около 15 минут — оформить карту.";
        }

        public async Task<int> SendPostMessageAsync(string message, string photoPath)
        {
            var counter = 0;
            var users = await _userManager.GetAllUsersAsync();
            foreach (var user in users)
            {
                try
                {
                    await SendMessage(user.TId, message, photoPath);
                    counter++;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("bot was blocked by the user"))
                        await _userManager.SetTrueToBotIsBannedAsync(user.Id);
                }
            }
            return counter;
        }

        private async Task SendMessage(long tid, string message, string photoPath)
        {
            var client = _bot.Get();

            if (!string.IsNullOrEmpty(photoPath))
            {
                using var stream = System.IO.File.Open(photoPath, FileMode.Open);
                await client.SendPhotoAsync(tid, InputFile.FromStream(stream), caption: message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
            }
            else
            {
                await client.SendTextMessageAsync(tid, message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown, disableWebPagePreview: true);
            }
        }
    }
}
