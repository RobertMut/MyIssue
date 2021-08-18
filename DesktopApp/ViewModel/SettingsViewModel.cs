﻿using Microsoft.Win32;
using MyIssue.Core.Entities;
using MyIssue.DesktopApp.Misc;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;
using MyIssue.Core.Interfaces;
using MyIssue.DesktopApp.Misc.Services;

namespace MyIssue.DesktopApp.ViewModel
{
    public class SettingsViewModel : BindableBase
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
            _settings.ConnectionMethod = false.ToString();
            _settings.SslTsl = false.ToString();
            _save = new SaveConfiguration();
        }
        private void LoadCommands()
        {
            SaveCommand = new DelegateCommand(SaveSettings, CanSaveSettings); //TODO: check if any value is null/empty
            SelectImageCommand = new DelegateCommand(SelectImage, CanSelectImage);
            ReturnToMain = new DelegateCommand(Return, CanReturn);
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
            _save.Save(Settings); 
        }
        private bool CanSaveSettings()
        {
            if (!string.IsNullOrEmpty(Settings.ApplicationPass) &&
               !string.IsNullOrEmpty(Settings.CompanyName) &&
               !string.IsNullOrEmpty(Settings.ConnectionMethod) &&
               !string.IsNullOrEmpty(Settings.EmailAddress) &&
               !string.IsNullOrEmpty(Settings.Login) &&
               !string.IsNullOrEmpty(Settings.Pass) &&
               !string.IsNullOrEmpty(Settings.Port)
               //!string.IsNullOrEmpty(Settings.RecipientAddress) &&
               //!string.IsNullOrEmpty(Settings.SslTsl)
               ) 
                return true;
            else
            {
                MessageBox.Show("Fill empty fields");
                return false;
            }
        }
        private void SelectImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            Settings.Image = dialog.ShowDialog() == true ? dialog.FileName : DependencyProperty.UnsetValue.ToString();
        }
        private bool CanSelectImage()
        {
            return true;
        }
        private void Return()
        {
            if (!(Settings is null))
            {
                var parameters = new NavigationParameters();
                parameters.Add("Settings", Settings);
                _regionManager.RequestNavigate("ContentRegion", "Main", Callback, parameters);
            }
            
        }
        private bool CanReturn()
        {
            return true;
        }
        private void Callback(NavigationResult res)
        {
            if (!(res.Error is null))
            {
                SerilogLoggerService.LogException(res.Error);
            }
        }

    }

}