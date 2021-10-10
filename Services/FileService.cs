using System;
using System.IO;
using System.Threading.Tasks;

namespace AWS_ECS_CoreApi.Services
{
    public class FileService : IFileService
    {
        public async Task WriteFile(Stream memoryStream, string fileName)
        {
            try
            {
                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", fileName);

                await using var stream = new FileStream(path, FileMode.Create);

                await memoryStream.CopyToAsync(stream);
            }
            catch (Exception e)
            {
            }
        }
    }
}