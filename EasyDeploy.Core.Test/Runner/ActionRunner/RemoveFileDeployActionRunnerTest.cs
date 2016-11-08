using EasyDeploy.Core.Model;
using EasyDeploy.Core.Runner.ActionRunner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDeploy.Core.Test.Runner.ActionRunner
{
    [TestClass]
    public class RemoveFileDeployActionRunnerTest
    {
        [TestMethod]
        public void TestRemoveFile()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "TestOutput", "delete.txt");
            File.Copy(Path.Combine(Directory.GetCurrentDirectory(), "TestData", "test.txt"), file, true);

            var action = new RemoveFileDeployAction
            {
                File = file
            };

            var runner = new RemoveFileDeployActionRunner();
            runner.Run(action);

            Assert.IsFalse(File.Exists(file));
        }

        [TestMethod]
        public void TestRemoveNonExistingFile()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "TestOutput", "delete.txt");
            if(File.Exists(file))
            {
                File.Delete(file);
            }

            var action = new RemoveFileDeployAction
            {
                File = file
            };

            var runner = new RemoveFileDeployActionRunner();
            runner.Run(action);

            Assert.IsFalse(File.Exists(file));
        }
    }
}
