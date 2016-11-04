using System.Collections.Generic;

namespace EasyDeploy.GUI.Settings
{
    public class SettingsContainer
    {
        private Settings settings = null;

        private readonly ISettingStorage storage;

        public SettingsContainer(ISettingStorage storage)
        {
            this.storage = storage;
        }

        public void Save()
        {
            storage.SaveSettings(settings);
        }

        private void InitialiseIfNecessay()
        {
            if(settings == null)
            {
                settings = storage.LoadSettings();
            }
        }

        public List<string> GetRecentFiles()
        {
            InitialiseIfNecessay();
            return settings.RecentFiles;
        }

        public void SetRecentFiles(List<string> recentFiles)
        {
            InitialiseIfNecessay();
            settings.RecentFiles.Clear();
            settings.RecentFiles.AddRange(recentFiles);
        }
    }
}
