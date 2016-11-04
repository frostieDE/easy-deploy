namespace EasyDeploy.Core.Model
{
    public class ExecuteFileDeployAction : DeployAction
    {
        public string File { get; set; }

        public string Arguments { get; set; }
    }
}
