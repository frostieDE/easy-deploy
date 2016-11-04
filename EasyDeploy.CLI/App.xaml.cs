using CommandLine;
using EasyDeploy.CLI.Parameters;
using EasyDeploy.Core.IO;
using EasyDeploy.Core.Json;
using EasyDeploy.Core.Logger;
using EasyDeploy.Core.Runner;
using System;
using System.Windows;

namespace EasyDeploy.CLI
{
    public partial class App : Application
    {
        public App()
        {
            var arguments = Environment.GetCommandLineArgs();
            var options = new Options();
            var parser = new Parser();

            if (!parser.ParseArguments(arguments, options))
            {
                Console.WriteLine("Command line parameters wrong!");
                Console.WriteLine(options.GetUsage());
                Environment.Exit(1);
            }

            var jsonReader = new JsonReader();
            var fileReader = new FileReader();
            ILogger logger = new NullLogger();

#if DEBUG
            logger = new ConsoleLogger();
#endif

            var runner = new Runner(logger, fileReader, jsonReader);
            runner.LoadConfiguration(options.ConfigurationFile);
            var resultCode = runner.Run();

            Environment.Exit(resultCode);
        }
    }
}
