namespace InstaDirect.Services.Interfaces
{
    public interface IAccountManager
    {
        public Task<bool> AnyFreeAccountsAsync();
        public Task<string> GetAccountDataAsync(long tid);
        public Task<bool> CheckForRecievedAccountTodayAsync(long tid);
        public Task<int> GetCountOfAccountsAsync();
        public Task<int> AddAccountsAsync(string accounts);
        public Task DeleteAllAccountsAsync();
    }
}
