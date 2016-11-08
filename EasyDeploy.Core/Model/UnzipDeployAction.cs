namespace EasyDeploy.Core.Model
{
    public class UnzipDeployAction : DeployAction
    {
        public string ZipFile { get; set; }

        public string Destination { get; set; }
    }
}
