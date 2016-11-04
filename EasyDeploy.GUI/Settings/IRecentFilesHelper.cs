using System.Collections.ObjectModel;

namespace EasyDeploy.GUI.Settings
{
    public interface IRecentFilesHelper
    {
        ObservableCollection<string> RecentFiles { get; }

        void Add(string file);

        void Clear();
    }
}
