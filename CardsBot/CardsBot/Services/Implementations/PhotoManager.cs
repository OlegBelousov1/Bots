using CardsBot.Services.Interfaces;
using System.Text;

namespace CardsBot.Services.Implementations
{
    public class PhotoManager : IPhotoManager
    {
        private readonly IWebHostEnvironment _environment;

        public PhotoManager(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SavePhotoAsync(IFormFile photo)
        {
            if (photo == null) return string.Empty;
            var photoName = GetPhotoName() + ".png";
            var photoPath = $"{_environment.WebRootPath}/photoes";
            if (!Directory.Exists(photoPath))
            {
                Directory.CreateDirectory(photoPath);
            }
            photoPath += "/" + photoName;
            using (var fileStream = new FileStream(photoPath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }
            return photoPath;
        }

        private static string GetPhotoName()
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringBuilder = new StringBuilder();
            var random = new Random();
            for (int index = 0; index < 11; index++)
                stringBuilder.Append(str[random.Next(str.Length)]);
            return stringBuilder.ToString();
        }
    }
}
