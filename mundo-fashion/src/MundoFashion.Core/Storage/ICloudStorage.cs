using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MundoFashion.Core.Storage
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(IFormFile imageFile, string fileNameStorage);
        Task DeleteFileAsync(string imageName);
        string GetImageUrl(string imageName);
    }
}
