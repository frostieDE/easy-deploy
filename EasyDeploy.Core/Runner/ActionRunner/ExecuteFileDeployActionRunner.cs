using EasyDeploy.Core.Model;
using System.Diagnostics;
using System.IO;

namespace EasyDeploy.Core.Runner.ActionRunner
{
    public class ExecuteFileDeployActionRunner : DeployActionRunner<ExecuteFileDeployAction>
    {
        protected override void Run(ExecuteFileDeployAction action)
        {
            if (File.Exists(action.File))
            {
                var processStartInfo = new ProcessStartInfo();
                processStartInfo.Arguments = action.Arguments;
                processStartInfo.FileName = action.File;
                processStartInfo.CreateNoWindow = false;
                processStartInfo.UseShellExecute = true;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                using (var process = Process.Start(processStartInfo))
                {
                    process.WaitForExit();
                }
            }
        }
    }
}
