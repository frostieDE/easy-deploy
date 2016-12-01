using System;
using System.IO;

namespace EasyDeploy.Core.Logger
{
    public class FileLogger : ILogger
    {
        private string path;

        public FileLogger(string path)
        {
            this.path = path.Replace("{Date}", DateTime.Today.ToString("yyyy-MM-dd"));
        }

        public void Exception(Exception exception)
        {
            WriteMessage($"Exception thrown of type {exception.GetType()}");
            WriteMessage($"Message: {exception.Message}");
            WriteMessage($"Stacktrace: {exception.StackTrace}");
        }

        public void Log(string message)
        {
            WriteMessage(message);
        }

        private void WriteMessage(params string[] messages)
        {
            var time = DateTime.Now.ToString("s");

            using (var writer = new StreamWriter(path, append: true))
            {
                foreach (var message in messages)
                {
                    writer.WriteLine($"[{time}] {message}");
                }
            }
        }
    }
}
