namespace EasyDeploy.GUI.UI.FilePicker
{
    public interface IFilePicker
    {
        string PickFolder();

        string PickFile(bool ensureFileExists);
    }
}
