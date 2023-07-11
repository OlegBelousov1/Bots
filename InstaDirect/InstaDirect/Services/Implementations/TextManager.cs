using InstaDirect.Models;
using InstaDirect.Repository;
using InstaDirect.Services.Interfaces;

namespace InstaDirect.Services.Implementations
{
    public class TextManager : ITextManager
    {
        private readonly IRepository<Text> _repository;

        public TextManager(IRepository<Text> repository)
        {
            _repository = repository;
        }

        public async Task EditStartTextAsync(Text text)
        {
            _repository.Update(text);
            await _repository.SaveChangesAsync();
        }

        public async Task<Text> GetStartTextAsync()
        {
            return await _repository.FirstOrDefaultAsync();
        }
    }
}
