namespace EasyDeploy.GUI.Settings
{
    public interface ISettingStorage
    {
        void SaveSettings(Settings settings);

        Settings LoadSettings();
    }
}
