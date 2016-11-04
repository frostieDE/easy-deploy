using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading.Tasks;

namespace EasyDeploy.GUI.UI.FilePicker
{
    public class FilePicker : IFilePicker
    {
        public string PickFile(bool ensureFileExists)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = false;
            dialog.Multiselect = false;
            dialog.EnsureFileExists = ensureFileExists;

            var result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }

            return null;
        }

        public string PickFolder()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;
            dialog.EnsureFileExists = true;

            var result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
