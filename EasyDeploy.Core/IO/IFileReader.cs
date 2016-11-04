using System.Threading.Tasks;

namespace EasyDeploy.Core.IO
{
    public interface IFileReader
    {
        Task<string> ReadFileAsync(string file);
    }
}
