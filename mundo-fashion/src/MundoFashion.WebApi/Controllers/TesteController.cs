using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MundoFashion.Infrastructure.Amazon.S3;
using MundoFashion.WebApi.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoFashion.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly FileUploader _fileUploader;

        public TesteController(FileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }

        [Route("upload-image")]
        [HttpPost]
        public async Task<string> UploadImage(IFormFile file)
        {
            await _fileUploader.UploadFile(file);
            return "Foi";
        }
    }
}
