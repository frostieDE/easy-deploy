using EasyDeploy.Core.Model;
using System.IO;

namespace EasyDeploy.Core.Runner.ActionRunner
{
    public class RemoveFolderDeployActionRunner : DeployActionRunner<RemoveFolderDeployAction>
    {
        protected override void Run(RemoveFolderDeployAction action)
        {
            if(Directory.Exists(action.Folder))
            {
                Directory.Delete(action.Folder, true);
            }
        }
    }
}
