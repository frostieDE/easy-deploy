namespace EasyDeploy.Core.Model
{
    public class CopyFileDeployAction : DeployAction
    {
        public bool OverrideTargetFile { get; set; }

        public string SourceFile { get; set; }

        public string TargetFile { get; set; }
    }
}
