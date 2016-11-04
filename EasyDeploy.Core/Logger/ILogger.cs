using System;

namespace EasyDeploy.Core.Logger
{
    public interface ILogger
    {
        void Log(string message);

        void Exception(Exception exception);
    }
}
