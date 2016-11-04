namespace EasyDeploy.Core.Runner
{
    public interface IRunner
    {
        void LoadConfiguration(string file);

        int Run();
    }
}
