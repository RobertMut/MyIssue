using MyIssue.DesktopApp.Model.Services;
using MyIssue.DesktopApp.Model.Utility;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyIssue.DesktopApp.ViewModel
{
    public class PromptViewModel : BindableBase, INavigationAware
    {
        private string _enteredPassword = string.Empty;
        private string _labelText;
        private string applicationPass;
        public DelegateCommand OpenSettings { get; private set; }
        public DelegateCommand ReturnToMainView { get; private set; }
        public DelegateCommand<object> GetPass { get; private set; }
        private IRegionManager _regionManager;
        public string LabelText
        {
            get
            {
                return _labelText;
            }
            private set
            {
                SetProperty(ref _labelText, value);
            }
        }
        public string EnteredPassword
        {
            get
            {
                return _enteredPassword;
            }
            private set
            {
                SetProperty(ref _enteredPassword, value);
                OpenSettings.RaiseCanExecuteChanged();
            }
        }
        public PromptViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoadCommands();
        }

        private void LoadCommands()
        {
            OpenSettings = new DelegateCommand(OpenSettingsButton, CanOpenSettingsButton);
            ReturnToMainView = new DelegateCommand(GoBack);
            GetPass = new DelegateCommand<object>(Pass);
        }
        private void Pass(object obj)
        {
            EnteredPassword = ((PasswordBox)obj).Password;
        }
        private void OpenSettingsButton()
        {
            _regionManager.RequestNavigate("ContentRegion", "SettingsView", Callback);
        }
        private bool CanOpenSettingsButton()
        {
            if(EnteredPassword.Equals(applicationPass)) return true;
            return false;
        }
        private void GoBack()
        {
            _regionManager.RequestNavigate("ContentRegion", "Main", Callback);
        }
        private void Callback(NavigationResult res)
        {
            if (!(res.Error is null))
            {
                SerilogLoggerService.LogException(res.Error);
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var appPass = navigationContext.Parameters["apppass"] as string;
            if (!(appPass is null))
            {
                applicationPass = appPass;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var appPass = navigationContext.Parameters["apppass"] as string;
            if (!(appPass is null)) return (applicationPass is null) && appPass.Equals(appPass);
            else return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //
        }
    }
}
