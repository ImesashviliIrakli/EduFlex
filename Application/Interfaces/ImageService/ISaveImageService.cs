using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.ImageService;

public interface ISaveImageService<T>
{
    Task<string> SaveImageAsync(IFormFile formFile, string fileName);
}
