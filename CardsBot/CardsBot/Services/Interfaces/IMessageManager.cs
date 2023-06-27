using CardsBot.Models;

namespace CardsBot.Services.Interfaces
{
    public interface IMessageManager
    {
        public Task<Message> GetMessageAsync(MessageType type);
        public Task<string> GetMessageTextAsync(MessageType type);
        public Task EditMessageAsync(string message, MessageType type);
        public Task<int> SendPostMessageAsync(string message, string photoPath);
    }
}
