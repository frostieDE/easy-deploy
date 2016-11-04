using System;

namespace EasyDeploy.GUI.Message
{
    public abstract class ErrorMessage
    {
        public Exception Exception { get; set; }
    }
}
