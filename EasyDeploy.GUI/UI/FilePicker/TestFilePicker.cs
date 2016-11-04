
namespace EasyDeploy.GUI.UI.FilePicker
{
    public class TestFilePicker : IFilePicker
    {
        public string File { get; set; }
        public string Folder { get; set; }

        public string PickFile(bool ensureFileExists)
        {
            return File;
        }

        public string PickFolder()
        {
            return Folder;
        }
    }
}
