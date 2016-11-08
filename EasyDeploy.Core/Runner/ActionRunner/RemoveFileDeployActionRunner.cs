using EasyDeploy.Core.Model;
using System.IO;

namespace EasyDeploy.Core.Runner.ActionRunner
{
    public class RemoveFileDeployActionRunner : DeployActionRunner<RemoveFileDeployAction>
    {
        protected override void Run(RemoveFileDeployAction action)
        {
            if (File.Exists(action.File))
            {
                File.Delete(action.File);
            }
        }
    }
}
