using EasyDeploy.Core.IO;
using EasyDeploy.Core.Json;
using EasyDeploy.Core.Logger;
using EasyDeploy.Core.Model;
using EasyDeploy.Core.Runner.ActionRunner;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyDeploy.Core.Runner
{
    public class Runner : IRunner
    {
        private List<IDeployActionRunner> Runners { get; } = new List<IDeployActionRunner>();

        private Configuration configuration;

        private readonly ILogger logger;
        private readonly IJsonReader jsonReader;
        private readonly IFileReader fileReader;

        public Runner(ILogger logger, IFileReader fileReader, IJsonReader jsonReader)
        {
            this.logger = logger;
            this.fileReader = fileReader;
            this.jsonReader = jsonReader;

            Runners.Add(new CopyFileDeployActionRunner());
            Runners.Add(new RemoveFileDeployActionRunner());
            Runners.Add(new ExecuteFileDeployActionRunner());
        }

        public void LoadConfiguration(string file)
        {
            logger.Log("Loading configuration...");
            var json = ReadJson(file);

            logger.Log("JSON file loaded");
            logger.Log("Parse JSON file");

            configuration = jsonReader.ReadJson<Configuration>(json);

            logger.Log("JSON file parsed");
        }

        public int Run()
        {
            var resultCode = 0;
            logger.Log("Running actions...");

            foreach(var action in configuration.Actions)
            {
                logger.Log($"Running action {action.Description}");

                var runner = Runners.FirstOrDefault(x => x.CanRun(action));

                if (runner == null)
                {
                    logger.Log("No runner found. Skipping");
                    continue;
                }

                try
                {
                    runner.Run(action);
                    logger.Log("Action successful");
                }
                catch (Exception e)
                {
                    logger.Exception(e);

                    if (!configuration.IsSoftFailureEnabled)
                    {
                        resultCode = 1;
                        break;
                    }
                }
            }

            logger.Log("All action performed");
            return resultCode;
        }

        private string ReadJson(string file)
        {
            var task = fileReader.ReadFileAsync(file);
            task.Wait();

            return task.Result;
        }
    }
}
