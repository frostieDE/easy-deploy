using System;

namespace EasyDeploy.Core.Logger
{
    public class NullLogger : ILogger
    {
        public void Exception(Exception exception) { }

        public void Log(string message) { }
    }
}
