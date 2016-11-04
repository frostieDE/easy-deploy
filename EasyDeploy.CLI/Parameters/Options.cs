using CommandLine;
using CommandLine.Text;

namespace EasyDeploy.CLI.Parameters
{
    public class Options
    {
        [Option('c', "configuration", Required = true, HelpText = "Path to the configuration file")]
        public string ConfigurationFile { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this);
        }
    }
}
