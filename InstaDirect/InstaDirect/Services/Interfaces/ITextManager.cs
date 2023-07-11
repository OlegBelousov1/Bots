using InstaDirect.Models;

namespace InstaDirect.Services.Interfaces
{
    public interface ITextManager
    {
        public Task<Text> GetStartTextAsync();
        public Task EditStartTextAsync(Text text);
    }
}
