using System.Threading.Tasks;

namespace EasyDeploy.Core.IO
{
    public interface IFileWriter
    {
        Task WriteFileAsync(string file, string content);
    }
}
