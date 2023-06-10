using SimbaBot.Models;
using SimbaBot.Services.Interfaces;

namespace SimbaBot.Services.Implementations
{
    public class PaymentManager : IPaymentManager
    {
        public OrderInformation GetInformationForPayment(int id)
        {
            return id switch
            {
                1 => new OrderInformation("Подписка", "1 месяц - 100 рублей 🔥", "1 месяц", 10000),
                2 => new OrderInformation("Подписка", "6 месяцев - 300 рублей 🔥🔥", "6 месяцев", 30000),
                3 => new OrderInformation("Подписка", "Год - 500 рублей 🔥🔥🔥", "Год", 50000),
                _ => new OrderInformation(),
            };
        }
    }
}
