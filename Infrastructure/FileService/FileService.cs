using Application.Interfaces.FileService;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.fileService
{
    public class FileService<T> : IFileService<T>
    {
        public async Task DeleteFileAsync(string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
            {
                await Task.CompletedTask;
            }

            if (!fileUrl.StartsWith("/"))
            {
                fileUrl = "/" + fileUrl;
            }

            string folderName = typeof(T).Name;
            string filePath = Path.Combine(@"C:\files", fileUrl.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            await Task.CompletedTask;
        }

        public async Task<string> SaveFileAsync(IFormFile formFile, string fileName)
        {
            string folderName = typeof(T).Name;
            string folderPath = Path.Combine(@"C:\files", folderName);
            string filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return $"/{folderName}/{fileName}";
        }

        public async Task<string> UpdateFileAsync(IFormFile formFile, string fileName, string fileUrl)
        {
            await DeleteFileAsync(fileUrl);

            var newFileurl = await SaveFileAsync(formFile, fileName);

            return newFileurl;
        }
    }
}
