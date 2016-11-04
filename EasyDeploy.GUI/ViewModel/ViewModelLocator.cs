/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:EasyDeploy.GUI"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using EasyDeploy.Core.IO;
using EasyDeploy.Core.Json;
using EasyDeploy.GUI.Settings;
using EasyDeploy.GUI.UI.FilePicker;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace EasyDeploy.GUI.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IMessenger, Messenger>();

            SimpleIoc.Default.Register<IJsonReader, JsonReader>();
            SimpleIoc.Default.Register<IJsonWriter, JsonWriter>();
            SimpleIoc.Default.Register<IFileReader, FileReader>();
            SimpleIoc.Default.Register<IFileWriter, FileWriter>();
            SimpleIoc.Default.Register<IFilePicker, FilePicker>();

            SimpleIoc.Default.Register<ISettingStorage, JsonSettingsStorage>();
            SimpleIoc.Default.Register<SettingsContainer>();
            SimpleIoc.Default.Register<IRecentFilesHelper, RecentFilesHelper>();

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}