using Application.Interfaces.ImageService;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.ImageService
{
    public class SaveImageService<T> : ISaveImageService<T>
    {
        public async Task<string> SaveImageAsync(IFormFile formFile, string fileName)
        {
            string folderName = typeof(T).Name;
            string folderPath = Path.Combine(@"C:\Images", folderName);
            string filePath = Path.Combine(folderPath, fileName);

            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Save the image file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return $"/{folderName}{fileName}";
        }
    }
}
