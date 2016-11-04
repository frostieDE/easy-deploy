using EasyDeploy.Core.Model;

namespace EasyDeploy.Core.Runner.ActionRunner
{
    public interface IDeployActionRunner
    {
        bool CanRun(DeployAction action);

        void Run(DeployAction action);
    }
}
