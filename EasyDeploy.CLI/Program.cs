using CommandLine;
using EasyDeploy.CLI.Parameters;
using EasyDeploy.Core.IO;
using EasyDeploy.Core.Json;
using EasyDeploy.Core.Logger;
using EasyDeploy.Core.Runner;
using System;

namespace EasyDeploy.CLI
{
    class Program
    {
        static int Main(string[] args)
        {
            var arguments = Environment.GetCommandLineArgs();
            var options = new Options();
            var parser = new Parser();

            var logger = new ConsoleLogger();

            if (!parser.ParseArguments(arguments, options))
            {
                logger.Log("Command line parameters wrong!");
                logger.Log(options.GetUsage());
                Environment.Exit(1);
            }

            var jsonReader = new DebugJsonReader(new JsonReader(), logger);
            var fileReader = new FileReader();

            var resultCode = 1;

            try
            {
                var runner = new Runner(logger, fileReader, jsonReader);
                runner.LoadConfiguration(options.ConfigurationFile);
                resultCode = runner.Run();
                logger.Log($"Result code: {resultCode}");
            }
            catch (Exception e)
            {
                logger.Exception(e);
            }

            return resultCode;
        }
    }
}
