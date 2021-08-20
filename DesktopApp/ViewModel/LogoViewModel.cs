using MyIssue.Core.Entities;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using MyIssue.DesktopApp.Misc;
using MyIssue.DesktopApp.Misc.Services;
using MyIssue.Infrastructure.Files;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyIssue.DesktopApp.ViewModel
{
    public class LogoViewModel
    {
        private IRegionManager _regionManager;
        private IDesktopData _data;
        private bool showedMessageBox = false;
        private SettingTextBoxes Settings { get; set; }
        private bool canEnterMain = false;
        public DelegateCommand GoToMain { get; private set; }

        public LogoViewModel(IRegionManager regionManager)
        {
            _data = new DesktopData();
            _regionManager = regionManager;
            LoadData();
            GoToMain = new DelegateCommand(Change);

        }

        private void LoadData()
        {
            try
            {
                Settings = _data.Load();
                canEnterMain = true;
            }
            catch (ConfigurationNotFoundException e)
            {
                Settings = null;
                canEnterMain = false;
                SerilogLoggerService.LogException(e);
            }
        }

        private void Change()
        {
            if (!File.Exists(Paths.confFile) || !canEnterMain)
            {
                ShowMessageBox();   
                _regionManager.RequestNavigate("ContentRegion", "SettingsView", Callback);
            } else
            {
                var parameters = new NavigationParameters();
                parameters.Add("Settings", Settings);
                _regionManager.RequestNavigate("ContentRegion", "Main", Callback, parameters);
            }
            
        }
        private void ShowMessageBox()
        {
            if (!showedMessageBox)
            MessageBox.Show("Configuration files does not exist\n Now you will be taken to settings", "Configuration not found", MessageBoxButton.OK, MessageBoxImage.Error);
            showedMessageBox = true;
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
