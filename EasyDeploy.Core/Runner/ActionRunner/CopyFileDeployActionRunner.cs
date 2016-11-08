using EasyDeploy.Core.Model;
using System.IO;

namespace EasyDeploy.Core.Runner.ActionRunner
{
    public class CopyFileDeployActionRunner : DeployActionRunner<CopyFileDeployAction>
    {
        protected override void Run(CopyFileDeployAction action)
        {
            if (File.Exists(action.SourceFile) && (!File.Exists(action.TargetFile) || action.OverrideTargetFile))
            {
                File.Copy(action.SourceFile, action.TargetFile, true);
            }
        }
    }
}
