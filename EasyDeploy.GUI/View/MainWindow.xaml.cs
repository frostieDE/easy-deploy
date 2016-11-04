using EasyDeploy.GUI.Message;
using EasyDeploy.GUI.ViewModel;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using System;

namespace EasyDeploy.GUI.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            Closing += OnClosing;
        }

        private async void OnClosing(object sender, CancelEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            var closeConfirmed = await viewModel.CloseConfigurationAsync();

            if(closeConfirmed == false)
            {
                e.Cancel = true;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            viewModel.Messenger.Register<CloseConfigurationConfigurationMessage>(this, OnCloseConfigurationConfigurationMessage);
            viewModel.Messenger.Register<OpeningFailedMessage>(this, OnOpeningFailedMessage);
            viewModel.Messenger.Register<SaveFailedMessage>(this, OnSaveFailedMessage);
        }

        private void OnSaveFailedMessage(SaveFailedMessage msg)
        {
            ShowErrorMessage(msg.Exception);
        }

        private void OnOpeningFailedMessage(OpeningFailedMessage msg)
        {
            ShowErrorMessage(msg.Exception);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            viewModel.Messenger.Unregister(this);
        }

        private void OnCloseConfigurationConfigurationMessage(CloseConfigurationConfigurationMessage obj)
        {
            var dialog = new TaskDialog();
            dialog.Opened += delegate
            {
                dialog.Icon = TaskDialogStandardIcon.Warning;
            };

            dialog.Icon = TaskDialogStandardIcon.Warning;

            dialog.Caption = Properties.Resources.CloseConfigurationTitle;
            dialog.Text = Properties.Resources.CloseConfigurationMessage;

            dialog.Cancelable = false;
            dialog.StartupLocation = TaskDialogStartupLocation.CenterOwner;
            dialog.OwnerWindowHandle = new WindowInteropHelper(this).Handle;
            dialog.StandardButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No;

            dialog.Show();
        }

        private void ShowErrorMessage(Exception e)
        {
            var dialog = new TaskDialog();
            dialog.Opened += delegate
            {
                dialog.Icon = TaskDialogStandardIcon.Error;
            };

            dialog.Icon = TaskDialogStandardIcon.Error;

            dialog.Caption = e.GetType().ToString();
            dialog.Text = e.Message;

            dialog.StartupLocation = TaskDialogStartupLocation.CenterOwner;
            dialog.OwnerWindowHandle = new WindowInteropHelper(this).Handle;

            dialog.Show();
        }

        private void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
