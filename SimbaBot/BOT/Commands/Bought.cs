using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Implementations;
using SimbaBot.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;

namespace SimbaBot.BOT.Commands
{
    public class Bought : Command
    {
        private readonly IPaymentManager _paymentManager;
        public Bought(IUserManager userManager, Bot bot, IRepository<BotUser> repository, IPaymentManager paymentManager) : base(userManager, bot, repository)
        {
            _paymentManager = paymentManager;
        }

        public override string Name => "Bought";

        public override async Task ExecuteAsync(Update message, TelegramBotClient client)
        {
            var tariff = Convert.ToInt32(message.CallbackQuery.Data.Split(',')[1]);
            var orderInfo = _paymentManager.GetInformationForPayment(tariff);
            var prices = new List<LabeledPrice>()
            {
                new LabeledPrice(orderInfo.Text,orderInfo.Price)
            };
            await client.SendInvoiceAsync(message.CallbackQuery.From.Id, orderInfo.Title, orderInfo.Description,
                message.CallbackQuery.From.Id.ToString(), "381764678:TEST:55396", "RUB", prices);
        }
    }
}
