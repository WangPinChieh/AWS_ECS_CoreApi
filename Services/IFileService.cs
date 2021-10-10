using System.IO;
using System.Threading.Tasks;

namespace AWS_ECS_CoreApi.Services
{
    public interface IFileService
    {
        Task WriteFile(Stream memoryStream, string fileName);
    }
}