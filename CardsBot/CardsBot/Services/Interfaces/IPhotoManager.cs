namespace CardsBot.Services.Interfaces
{
    public interface IPhotoManager
    {
        public Task<string> SavePhotoAsync(IFormFile photo);
    }
}
