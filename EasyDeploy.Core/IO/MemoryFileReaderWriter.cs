using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyDeploy.Core.IO
{
    public class MemoryFileReaderWriter : IFileReader, IFileWriter
    {
        private readonly Dictionary<string, string> Contents = new Dictionary<string, string>();

        public Task<string> ReadFileAsync(string file)
        {
            if (Contents.ContainsKey(file))
            {
                return Task.FromResult(Contents[file]);
            }

            string result = null;
            return Task.FromResult(result);
        }

        public Task WriteFileAsync(string file, string content)
        {
            Contents.Add(file, content);
            return Task.Delay(0);
        }
    }
}
