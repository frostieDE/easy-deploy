using System.IO;
using System.Threading.Tasks;

namespace EasyDeploy.Core.IO
{
    public class FileReader : IFileReader
    {
        public async Task<string> ReadFileAsync(string file)
        {
            using (var reader = new StreamReader(file))
            {
                return await reader.ReadToEndAsync().ConfigureAwait(false);
            }
        }
    }
}
