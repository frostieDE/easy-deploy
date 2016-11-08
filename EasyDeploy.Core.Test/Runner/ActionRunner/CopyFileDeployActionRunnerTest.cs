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
    public class CopyFileDeployActionRunnerTest
    {
        [TestMethod]
        public void TestCopyFile()
        {
            var targetFile = Path.Combine(Directory.GetCurrentDirectory(), "TestOutput", "test.txt");

            if(File.Exists(targetFile))
            {
                File.Delete(targetFile);
            }

            var action = new CopyFileDeployAction
            {
                SourceFile = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "test.txt"),
                TargetFile = targetFile
            };

            var runner = new CopyFileDeployActionRunner();
            runner.Run(action);

            Assert.IsTrue(File.Exists(targetFile));
        }

        [TestMethod]
        public void TestCopyFileWithOverride()
        {
            var targetFile = Path.Combine(Directory.GetCurrentDirectory(), "TestOutput", "test.txt");

            if (!File.Exists(targetFile))
            {
                File.Copy(Path.Combine(Directory.GetCurrentDirectory(), "TestData", "test.txt"), targetFile);
            }

            var action = new CopyFileDeployAction
            {
                SourceFile = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "test2.txt"),
                TargetFile = targetFile,
                OverrideTargetFile = true
            };

            var runner = new CopyFileDeployActionRunner();
            runner.Run(action);

            Assert.IsTrue(File.Exists(targetFile));
            Assert.AreEqual("Test2", File.ReadAllText(targetFile));
        }

        [TestMethod]
        public void TestCopyFileWithoutOverride()
        {
            var targetFile = Path.Combine(Directory.GetCurrentDirectory(), "TestOutput", "test.txt");
            File.Copy(Path.Combine(Directory.GetCurrentDirectory(), "TestData", "test.txt"), targetFile, true);

            var action = new CopyFileDeployAction
            {
                SourceFile = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "test2.txt"),
                TargetFile = targetFile,
                OverrideTargetFile = false
            };

            var runner = new CopyFileDeployActionRunner();
            runner.Run(action);

            Assert.IsTrue(File.Exists(targetFile));
            Assert.AreEqual("Test", File.ReadAllText(targetFile));
        }
    }
}
