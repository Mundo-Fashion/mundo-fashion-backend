using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MundoFashion.Core.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MundoFashion.Infrastructure.Storage.Google
{
    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly GoogleCredential _googleCredential;
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public GoogleCloudStorage(IConfiguration configuration)
        {
            string credentialFileFromEnv = Environment.GetEnvironmentVariable("GOOGLE_CREDENTIAL_FILE");
            string credentialFile = credentialFileFromEnv ?? configuration["GoogleCredentialFile"];

            _googleCredential = string.IsNullOrWhiteSpace(credentialFileFromEnv) ? GoogleCredential.FromFile(credentialFile) : GoogleCredential.FromJson(credentialFile);
            _storageClient = StorageClient.Create(_googleCredential);
            _bucketName = configuration["GoogleCloudStorageBucket"];
        }

        public async Task<string> UploadFileAsync(IFormFile imageFile, string fileNameStorage)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                var dataObject = await _storageClient.UploadObjectAsync(_bucketName, fileNameStorage, null, memoryStream);
                return GetImageUrl(fileNameStorage);
            }
        }

        public async Task DeleteFileAsync(string imageName)
        {
            await _storageClient.DeleteObjectAsync(_bucketName, imageName);
        }

        public string GetImageUrl(string imageName)
            => @$"http://{_bucketName}.storage.googleapis.com/{imageName}";
    }
}
