using EasyDeploy.Core.IO;
using EasyDeploy.Core.Json;
using EasyDeploy.Core.Model;
using EasyDeploy.GUI.UI.FilePicker;
using EasyDeploy.GUI.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyDeploy.GUI.Test.ViewModel
{
    [TestClass]
    public class MainViewModelTest
    {
        [TestMethod]
        public void TestPossibleDeployActionsNotEmpty()
        {
            var viewModel = new MainViewModel(null, null, null, null, null, null, null);
            Assert.AreNotEqual(0, viewModel.PossibleDeployActions.Count);
        }
        
        [TestMethod]
        public void TestNewConfiguration()
        {
            var jsonReader = new JsonReader();
            var jsonWriter = new JsonWriter();

            var fileReaderWriter = new MemoryFileReaderWriter();

            var filePicker = new TestFilePicker();
            filePicker.File = "./test.json";

            var viewModel = new MainViewModel(filePicker, fileReaderWriter, jsonWriter, fileReaderWriter, jsonReader, null, null);
            viewModel.NewConfigurationCommand.Execute(null);

            Assert.IsNotNull(viewModel.Configuration);
            Assert.IsNull(viewModel.CurrentAction);
            Assert.AreEqual(0, viewModel.DeployActions.Count);
        }

        [TestMethod]
        public void TestAddDeployAction()
        {
            var jsonReader = new JsonReader();
            var jsonWriter = new JsonWriter();

            var fileReaderWriter = new MemoryFileReaderWriter();

            var filePicker = new TestFilePicker();
            filePicker.File = "./test.json";

            var viewModel = new MainViewModel(filePicker, fileReaderWriter, jsonWriter, fileReaderWriter, jsonReader, null, null);
            viewModel.NewConfigurationCommand.Execute(null);

            var action = new CopyFileDeployAction();
            viewModel.AddActionCommand.Execute(action);

            Assert.IsNotNull(viewModel.CurrentAction);
            Assert.AreEqual(action, viewModel.CurrentAction);
        }
    }
}
