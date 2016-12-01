using System;

namespace EasyDeploy.Core.Logger
{
    public class DebugLogger : ILogger
    {
        public void Exception(Exception exception)
        {
            Log(exception.Message);
        }

        public void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
