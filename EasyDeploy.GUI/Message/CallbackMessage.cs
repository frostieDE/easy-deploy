using System;

namespace EasyDeploy.GUI.Message
{
    public abstract class CallbackMessage
    {
        public Action SuccessAction { get; set; }
        public Action CancelAction { get; set; }
    }
}
