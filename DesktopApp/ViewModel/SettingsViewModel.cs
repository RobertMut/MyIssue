using Microsoft.Win32;
using MyIssue.Core.Entities;
using MyIssue.DesktopApp.Misc;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;
using MyIssue.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using MyIssue.Infrastructure.Files;

namespace MyIssue.DesktopApp.ViewModel
{
    public class SettingsViewModel : BindableBase, INavigationAware
    {
        private SettingTextBoxes _settings;
        private IRegionManager _regionManager;
        private ISaveConfiguration _save;

        public SettingTextBoxes Settings
        {
            get { return _settings; }
            set
            {
                SetProperty(ref _settings, value);
            }
        }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand SelectImageCommand { get; private set; }
        public DelegateCommand ReturnToMain { get; private set; }
        public DelegateCommand<object> GetAppPass { get; set; }
        public DelegateCommand<object> GetPass { get; set; }
        public SettingsViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoadCommands();
            _settings = new SettingTextBoxes();
            Settings.ConnectionMethod = false.ToString();
            Settings.SslTsl = false.ToString();
            _save = new SaveConfiguration();
        }
        private void LoadCommands()
        {
            SaveCommand = new DelegateCommand(SaveSettings);
            SelectImageCommand = new DelegateCommand(SelectImage);
            ReturnToMain = new DelegateCommand(Return);
            GetAppPass = new DelegateCommand<object>(AppPass);
            GetPass = new DelegateCommand<object>(Pass);
        }

        private void Pass(object obj)
        {
            Settings.Pass = ((PasswordBox)obj).Password;
        }

        private void AppPass(object obj)
        {
            Settings.ApplicationPass = ((PasswordBox)obj).Password;
        }

        private void SaveSettings()
        {
            List<string> textboxes = new List<string>() {
                    Settings.ApplicationPass,
                    Settings.CompanyName,
                    Settings.Login,
                    Settings.Pass,
                    Settings.Port,
                    Settings.ServerAddress,
                    Settings.EmailAddress,
                    Settings.RecipientAddress,
                    Settings.SslTsl
                    };
            switch (Settings.ConnectionMethod)
            {
                case "True":

                    if (textboxes.Any(t => string.IsNullOrEmpty(t)))
                        ShowMessageBox();
                    else
                        _save.Save(Settings);
                    break;
                case "False":
                    if (textboxes.Take(6).Any(t => string.IsNullOrEmpty(t)))
                        ShowMessageBox();
                    else
                        _save.Save(Settings);
                    break;
                default:
                    ShowMessageBox();
                    break;
            }
        }
        private void ShowMessageBox()
        {
            MessageBox.Show("Fill empty fields", "Form not filled", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        private void SelectImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            Settings.Image = dialog.ShowDialog() == true ? dialog.FileName : DependencyProperty.UnsetValue.ToString();
        }
        private void Return()
        {
            if (!(Settings is null))
            {
                var parameters = new NavigationParameters();
                parameters.Add("Settings", Settings);
                _regionManager.RequestNavigate("ContentRegion", "Main", Callback, parameters);
            }
            else
                ShowMessageBox();

        }
        private void Callback(NavigationResult res)
        {
            if (!(res.Error is null))
            {
                SerilogLogger.ClientLogException(res.Error);
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var settings = navigationContext.Parameters["Settings"] as SettingTextBoxes;
            if (!(settings is null))
            {
                Settings = settings;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var settings = navigationContext.Parameters["Settings"] as SettingTextBoxes;
            if (!(settings is null)) return (Settings is null) && settings.Equals(Settings);
            else return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }

}