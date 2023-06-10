using SimbaBot.Models;

namespace SimbaBot.Services.Interfaces
{
    public interface IPaymentManager
    {
        public OrderInformation GetInformationForPayment(int id);
    }
}
