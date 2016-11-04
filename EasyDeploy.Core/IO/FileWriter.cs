using System.IO;
using System.Threading.Tasks;

namespace EasyDeploy.Core.IO
{
    public class FileWriter : IFileWriter
    {
        public async Task WriteFileAsync(string file, string content)
        {
            using (var writer = new StreamWriter(file))
            {
                await writer.WriteAsync(content).ConfigureAwait(false);
            }
        }
    }
}
