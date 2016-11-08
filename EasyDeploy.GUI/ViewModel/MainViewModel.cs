using EasyDeploy.Core.IO;
using EasyDeploy.Core.Json;
using EasyDeploy.Core.Model;
using EasyDeploy.GUI.Message;
using EasyDeploy.GUI.Settings;
using EasyDeploy.GUI.UI.FilePicker;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace EasyDeploy.GUI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Recent files

        public ObservableCollection<string> RecentFiles { get; } = new ObservableCollection<string>();

        #endregion

        public List<DeployAction> PossibleDeployActions { get; } = new List<DeployAction>();

        public ObservableCollection<DeployAction> DeployActions { get; } = new ObservableCollection<DeployAction>();

        private DeployAction currentAction;

        public DeployAction CurrentAction
        {
            get { return currentAction; }
            set
            {
                Set(() => CurrentAction, ref currentAction, value);

                RemoveActionCommand?.RaiseCanExecuteChanged();
                MoveUpCommand?.RaiseCanExecuteChanged();
                MoveDownCommand?.RaiseCanExecuteChanged();
            }
        }

        private Configuration configuration;

        public Configuration Configuration
        {
            get { return configuration; }
            set
            {
                Set(() => Configuration, ref configuration, value);

                SaveConfigurationCommand?.RaiseCanExecuteChanged();
                AddActionCommand?.RaiseCanExecuteChanged();
                RemoveActionCommand?.RaiseCanExecuteChanged();
                MoveUpCommand?.RaiseCanExecuteChanged();
                MoveDownCommand?.RaiseCanExecuteChanged();
            }
        }

        private string configurationFile;

        #region Commands

        public RelayCommand NewConfigurationCommand { get; private set; }
        public RelayCommand<string> LoadConfigurationCommand { get; private set; }
        public RelayCommand SaveConfigurationCommand { get; private set; }

        public RelayCommand<DeployAction> AddActionCommand { get; private set; }
        public RelayCommand<DeployAction> RemoveActionCommand { get; private set; }
        public RelayCommand<DeployAction> MoveUpCommand { get; private set; }
        public RelayCommand<DeployAction> MoveDownCommand { get; private set; }

        public RelayCommand<DeployAction> SaveActionCommand { get; private set; }

        #endregion

        #region Services 

        private readonly IFilePicker filePicker;
        private readonly IFileWriter fileWriter;
        private readonly IJsonWriter jsonWriter;
        private readonly IFileReader fileReader;
        private readonly IJsonReader jsonReader;

        private readonly IRecentFilesHelper recentFilesHelper;

        public IMessenger Messenger { get { return base.MessengerInstance; } }

        #endregion

        public MainViewModel(IFilePicker filePicker, IFileWriter fileWriter, IJsonWriter jsonWriter, IFileReader fileReader, IJsonReader jsonReader, IRecentFilesHelper recentFilesHelper, IMessenger messenger)
            : base(messenger)
        {
            this.filePicker = filePicker;
            this.fileWriter = fileWriter;
            this.jsonWriter = jsonWriter;
            this.fileReader = fileReader;
            this.jsonReader = jsonReader;

            this.recentFilesHelper = recentFilesHelper;

            NewConfigurationCommand = new RelayCommand(CreateNewConfiguration);
            LoadConfigurationCommand = new RelayCommand<string>(LoadConfiguration);
            SaveConfigurationCommand = new RelayCommand(SaveConfiguration, CanSaveConfiguration);

            AddActionCommand = new RelayCommand<DeployAction>(AddAction, CanAddAction);
            RemoveActionCommand = new RelayCommand<DeployAction>(RemoveAction, CanRemoveAction);
            MoveUpCommand = new RelayCommand<DeployAction>(MoveUp, CanMoveUp);
            MoveDownCommand = new RelayCommand<DeployAction>(MoveDown, CanMoveDown);

            AddPossibleDeployActions();

            DeployActions.CollectionChanged += OnDeployActionChanged;
            
            if(this.recentFilesHelper != null)
            {
                SyncRecentFiles();
                this.recentFilesHelper.RecentFiles.CollectionChanged += OnRecentFilesCollectionChanged;
            }
        }

        private void OnRecentFilesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SyncRecentFiles();
        }

        private void SyncRecentFiles()
        {
            if(recentFilesHelper == null)
            {
                return;
            }

            RecentFiles.Clear();

            foreach(var file in recentFilesHelper.RecentFiles)
            {
                RecentFiles.Add(file);
            }
        }

        private void OnDeployActionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RemoveActionCommand?.RaiseCanExecuteChanged();
            MoveUpCommand?.RaiseCanExecuteChanged();
            MoveDownCommand?.RaiseCanExecuteChanged();
        }

        private void AddPossibleDeployActions()
        {
            PossibleDeployActions.Clear();
            PossibleDeployActions.Add(new CopyFileDeployAction());
            PossibleDeployActions.Add(new RemoveFileDeployAction());
            PossibleDeployActions.Add(new ExecuteFileDeployAction());
            PossibleDeployActions.Add(new UnzipDeployAction());
        }

        private async void CreateNewConfiguration()
        {
            var closeConfirmed = await CloseConfigurationAsync();
            if (closeConfirmed == false)
            {
                return;
            }

            Configuration = new Configuration();
            PopulateDeployActions();
        }

        private async void LoadConfiguration(string file)
        {
            var closeConfirmed = await CloseConfigurationAsync();
            if(closeConfirmed == false)
            {
                return;
            }

            if (file == null)
            {
                file = filePicker.PickFile(true);
            }

            if(file != null)
            {
                try
                {
                    configurationFile = file;
                    var json = await fileReader.ReadFileAsync(file);
                    Configuration = await Task.Run(() => jsonReader.ReadJson<Configuration>(json));

                    recentFilesHelper?.Add(file);
                }
                catch (Exception e)
                {
                    Messenger.Send(new OpeningFailedMessage());
                }
            }

            PopulateDeployActions();
        }

        private async void SaveConfiguration()
        {
            if(configurationFile == null)
            {
                configurationFile = filePicker.PickFile(false);
            }

            Configuration.Actions = DeployActions.ToList();

            try
            {
                var json = await Task.Run(() => jsonWriter.WriteJson(Configuration));
                await fileWriter.WriteFileAsync(configurationFile, json);
            }
            catch (Exception e)
            {
                Messenger.Send(new SaveFailedMessage());
            }
        }

        private bool CanSaveConfiguration() => Configuration != null;

        private void AddAction(DeployAction action)
        {
            DeployActions.Add(action);
            CurrentAction = action;
        }

        private bool CanAddAction(DeployAction action) => Configuration != null;

        private void RemoveAction(DeployAction action)
        {
            DeployActions.Remove(action);
        }

        private bool CanRemoveAction(DeployAction action) => Configuration != null && action != null && DeployActions.Contains(action);

        private void MoveUp(DeployAction action)
        {
            var oldIndex = DeployActions.IndexOf(action);
            var newIndex = oldIndex - 1;

            DeployActions.Move(oldIndex, newIndex);
        }

        private bool CanMoveUp(DeployAction action) => Configuration != null && action != null && DeployActions.IndexOf(action) > 0;

        private void MoveDown(DeployAction action)
        {
            var oldIndex = DeployActions.IndexOf(action);
            var newIndex = oldIndex + 1;

            DeployActions.Move(oldIndex, newIndex);
        }

        private bool CanMoveDown(DeployAction action) => Configuration != null && action != null && DeployActions.IndexOf(action) < DeployActions.Count - 1;

        private void CloseConfiguration()
        {
            Configuration = null;
            CurrentAction = null;
            DeployActions.Clear();
        }

        public Task<bool> CloseConfigurationAsync()
        {
            if(Configuration == null)
            {
                CloseConfiguration();
                return Task.FromResult(true);
            }

            var taskCompletionSource = new TaskCompletionSource<bool>();
            var message = new CloseConfigurationConfigurationMessage();

            message.SuccessAction = () =>
            {
                CloseConfiguration();
                taskCompletionSource.SetResult(true);
            };

            message.CancelAction = () =>
            {
                taskCompletionSource.SetResult(false);
            };

            Messenger.Send(message);

            return taskCompletionSource.Task;
        }

        private void PopulateDeployActions()
        {
            foreach (var action in Configuration.Actions)
            {
                DeployActions.Add(action);
            }
        }
    }
}