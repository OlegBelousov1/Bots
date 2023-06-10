using SimbaBot.BOT;
using SimbaBot.Models;
using SimbaBot.Repository;
using SimbaBot.Services.Interfaces;
using Telegram.Bot;

namespace SimbaBot.Services.Implementations
{
    public class WithdrawalManager : IWithdrawalManager
    {
        private readonly IRepository<Withdrawal> _repository;
        private readonly IUserManager _userManager;
        private readonly Bot _bot;

        public WithdrawalManager(IRepository<Withdrawal> repository, IUserManager userManager, Bot bot)
        {
            _repository = repository;
            _userManager = userManager;
            _bot = bot;
        }

        public async Task AddWithdrawalAsync(Withdrawal withdrawal)
        {
            await _repository.AddAsync(withdrawal);
            await _repository.SaveChangesAsync();
        }

        public async Task CompleteWithdrawalAsync(int id)
        {
            var withdrawal = await _repository.FirstOrDefaultAsync(i => i.Id == id);
            if (withdrawal != null)
            {
                var user = await _userManager.GetBotUserAsync(withdrawal.Tid);
                user.Balance = 0;
                user.Status = UserStatus.Default;
                await _userManager.EditUserAsync(user);
                _repository.Remove(withdrawal);
                await _repository.SaveChangesAsync();
                var bot = _bot.Get();
                await bot.SendTextMessageAsync(user.TId, "Ваша заявка на вывод баланса одобрена");
            }
        }

        public async Task<IEnumerable<Withdrawal>> GetWithdrawalsAsync()
        {
            return await _repository.ToListAsync();
        }
    }
}
