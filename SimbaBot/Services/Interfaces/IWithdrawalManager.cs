using SimbaBot.Models;

namespace SimbaBot.Services.Interfaces
{
    public interface IWithdrawalManager
    {
        public Task AddWithdrawalAsync(Withdrawal withdrawal);
        public Task<IEnumerable<Withdrawal>> GetWithdrawalsAsync();
        public Task CompleteWithdrawalAsync(int id);
    }
}
