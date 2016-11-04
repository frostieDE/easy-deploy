using System.Collections.ObjectModel;
using System.Linq;

namespace EasyDeploy.GUI.Settings
{
    public class RecentFilesHelper : IRecentFilesHelper
    {
        public ObservableCollection<string> RecentFiles { get; } = new ObservableCollection<string>();

        private readonly SettingsContainer settingsContainer;

        public RecentFilesHelper(SettingsContainer settingsContainer)
        {
            this.settingsContainer = settingsContainer;
            Initialise();
        }

        private void Initialise()
        {
            foreach (var file in settingsContainer.GetRecentFiles())
            {
                RecentFiles.Add(file);
            }
        }

        private void Save()
        {
            settingsContainer.SetRecentFiles(RecentFiles.ToList());
            settingsContainer.Save();
        }

        public void Add(string file)
        {
            RecentFiles.Insert(0, file);
            Save();
        }

        public void Clear()
        {
            RecentFiles.Clear();
            Save();
        }
    }
}
