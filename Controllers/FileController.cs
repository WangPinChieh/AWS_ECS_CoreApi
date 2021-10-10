using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AWS_ECS_CoreApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS_ECS_CoreApi.Controllers
{
    [ApiController]
    [Route("File/Upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
        {
            Thread.Sleep(new Random().Next(1, 20) * 1000);
            await using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream, cancellationToken);
                await _fileService.WriteFile(memoryStream, file.FileName);
            }

            return Content(JsonSerializer.Serialize(new {file.FileName, TimeStamp = Request.Form["token"][0]}),
                "application/json");
        }
    }
}