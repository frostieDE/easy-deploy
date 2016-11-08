using EasyDeploy.Core.Model;
using EasyDeploy.Core.Runner.ActionRunner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EasyDeploy.Core.Test.Runner.ActionRunner
{
    [TestClass]
    public class UnzipDeployActionRunnerTest
    {
        [TestMethod]
        public void TestUnzip()
        {
            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "TestOutput", "test");
            if(Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }

            var action = new UnzipDeployAction
            {
                ZipFile = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "test.zip"),
                Destination = outputPath
            };

            var runner = new UnzipDeployActionRunner();
            runner.Run(action);

            Assert.IsTrue(Directory.Exists(outputPath));
            Assert.IsTrue(Directory.Exists(Path.Combine(outputPath, "foo")));
            Assert.IsTrue(File.Exists(Path.Combine(outputPath, "foo", "bla.txt")));
            Assert.IsTrue(File.Exists(Path.Combine(outputPath, "bla.txt")));
        }

        [TestMethod]
        public void TestUnzipCreateMultipleFolders()
        {
            var nestedPath = Path.Combine(Directory.GetCurrentDirectory(), "TestOutput", "nestedFolder");
            var outputPath = Path.Combine(nestedPath, "test");
            if (Directory.Exists(nestedPath))
            {
                Directory.Delete(nestedPath, true);
            }

            var action = new UnzipDeployAction
            {
                ZipFile = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "test.zip"),
                Destination = outputPath
            };

            var runner = new UnzipDeployActionRunner();
            runner.Run(action);

            Assert.IsTrue(Directory.Exists(outputPath));
            Assert.IsTrue(Directory.Exists(Path.Combine(outputPath, "foo")));
            Assert.IsTrue(File.Exists(Path.Combine(outputPath, "foo", "bla.txt")));
            Assert.IsTrue(File.Exists(Path.Combine(outputPath, "bla.txt")));
        }
    }
}
