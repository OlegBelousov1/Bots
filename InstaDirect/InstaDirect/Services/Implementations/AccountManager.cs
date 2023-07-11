using InstaDirect.Models;
using InstaDirect.Repository;
using InstaDirect.Services.Interfaces;
using System.Security.Cryptography;
using Telegram.Bot.Types;

namespace InstaDirect.Services.Implementations
{
    public class AccountManager : IAccountManager
    {
        private readonly IUserManager _userManager;
        private readonly IRepository<Account> _repository;

        public AccountManager(IUserManager userManager, IRepository<Account> repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<int> GetCountOfAccountsAsync()
        {
            var accounts = await _repository.ToListAsync();
            return accounts.Count;
        }

        public async Task<bool> AnyFreeAccountsAsync()
        {
            var check = await _repository.FirstOrDefaultAsync();
            return check != null;
        }

        public async Task<bool> CheckForRecievedAccountTodayAsync(long tid)
        {
            var user = await _userManager.GetUserByTIdAsync(tid);
            return user.TId != 1423578631 && user.TId != 1184327996 && user.LastTake.AddDays(1) > DateTime.Now;
        }

        public async Task<string> GetAccountDataAsync(long tid)
        {
            var user = await _userManager.GetUserByTIdAsync(tid);
            var accounts = await _repository.ToListAsync();
            var random = new Random();
            var choosenAcc = accounts[random.Next(0, accounts.Count)];
            _repository.Remove(choosenAcc);
            await _repository.SaveChangesAsync(); 
            user.LastTake = DateTime.Now;
            await _userManager.EditUserAsync(user);
            return choosenAcc.AccountString;
        }

        public async Task<int> AddAccountsAsync(string accounts)
        {
            var count = 0;
            var accs = accounts.Split('\n');
            foreach (var acc in accs)
            {
                await _repository.AddAsync(new Account { AccountString = acc });
                count++;
            }
            await _repository.SaveChangesAsync();
            return count;
        }

        public async Task DeleteAllAccountsAsync()
        {
            var accounts = await _repository.ToListAsync();
            _repository.RemoveRange(accounts);
            await _repository.SaveChangesAsync();
        }
    }
}
