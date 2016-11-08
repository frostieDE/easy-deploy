using EasyDeploy.Core.Model;
using System.IO;
using System.IO.Compression;

namespace EasyDeploy.Core.Runner.ActionRunner
{
    public class UnzipDeployActionRunner : DeployActionRunner<UnzipDeployAction>
    {
        protected override void Run(UnzipDeployAction action)
        {
            if(!Directory.Exists(action.Destination))
            {
                Directory.CreateDirectory(action.Destination);
            }

            ZipFile.ExtractToDirectory(action.ZipFile, action.Destination);
        }
    }
}
