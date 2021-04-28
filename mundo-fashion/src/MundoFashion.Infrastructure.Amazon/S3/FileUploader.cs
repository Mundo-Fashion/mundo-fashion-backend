using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MundoFashion.Infrastructure.Amazon.S3
{
    public class FileUploader
    {
        private readonly IConfiguration _configuration;
        public FileUploader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            using (AmazonS3Client amazonClient = new AmazonS3Client(_configuration["AWS:AwsAccessKeyId"], _configuration["AWS:AwsSecretAccessKey"], RegionEndpoint.USEast1))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    file.CopyTo(stream);

                    string fileName = Guid.NewGuid().ToString();

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = stream,
                        Key = fileName,
                        BucketName = _configuration["AWS:S3:BucketName"],
                        CannedACL = S3CannedACL.PublicRead
                    }; 

                    TransferUtility fileTransferUtility = new TransferUtility(amazonClient);
                    await fileTransferUtility.UploadAsync(uploadRequest);

                    return fileName;
                }
            }
        }

        public async Task DeleteFile(string fileName)
        {
            using (AmazonS3Client amazonClient = new AmazonS3Client(_configuration["AWS:AwsAccessKeyId"], _configuration["AWS:AwsAccessKeySecret"]))
                await amazonClient.DeleteObjectAsync(_configuration["AWS:S3:BucketName"], fileName);
        }
    }
}
