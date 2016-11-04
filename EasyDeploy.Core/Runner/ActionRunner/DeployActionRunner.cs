using EasyDeploy.Core.Model;

namespace EasyDeploy.Core.Runner.ActionRunner
{
    public abstract class DeployActionRunner<T> : IDeployActionRunner
        where T : DeployAction
    {
        public bool CanRun(DeployAction action)
        {
            return action is T;
        }

        public void Run(DeployAction action)
        {
            Run(action as T);
        }

        protected abstract void Run(T action);
    }
}
