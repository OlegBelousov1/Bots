namespace InstaDirect.Services.Interfaces
{
    public interface IMessageManager
    {
        public Task<int> SendMessagesAsync(string message);
    }
}
