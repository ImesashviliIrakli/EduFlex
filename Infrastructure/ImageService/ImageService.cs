using Application.Interfaces.ImageService;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.ImageService
{
    public class SaveImageService<T> : ISaveImageService<T>
    {
        public Task<string> SaveImageAsync(IFormFile formFile, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
