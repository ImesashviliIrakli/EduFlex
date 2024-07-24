using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.FileService;

public interface IFileService<T>
{
    Task<string> SaveFileAsync(IFormFile formFile, string fileName);

    Task<string> UpdateFileAsync(IFormFile formFile, string fileName, string fileUrl);

    Task DeleteFileAsync(string fileUrl);
}
