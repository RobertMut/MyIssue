using MyIssue.Core.Entities;
using MyIssue.DesktopApp.Model.Exceptions;
using MyIssue.DesktopApp.Model.Services;
using MyIssue.DesktopApp.Model.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyIssue.DesktopApp.ViewModel
{
    public class SettingsWindowViewModel : INotifyPropertyChanged
    {
        private SettingTextBoxes settings;
        private IWindowService _windowService;
        //private IDesktopExceptionHandler _exceptionHandler;
        public event PropertyChangedEventHandler PropertyChanged;
        public SettingTextBoxes Settings
        {
            get { return settings; }
            set
            {
                settings = value;
                RaisePropertyChanged("Settings");
            }
        }
        public ICommand SaveCommand { get; set; }
        public ICommand SelectImageCommand { get; set; }
        public ICommand ClosingCommand { get; set; }
        public SettingsWindowViewModel(IWindowService service)
        {
            //_exceptionHandler = new DesktopExceptionHandler();
            _windowService = service;
            LoadCommands();
        }
        private void LoadCommands()
        {
            SaveCommand = new CustomCommands(SaveSettings, CanSaveSettings);
            SelectImageCommand = new CustomCommands(SelectImage, CanSelectImage);
            ClosingCommand = new CustomCommands(Closing, CanBeClosed);
        }
        private void RaisePropertyChanged(string property)
        {
            if (!(PropertyChanged is null)) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        private void SaveSettings(object obj)
        {
            Messenger.Default.Send<SettingTextBoxes>(Settings);

            _windowService.ShowPrompt();
        }
        private bool CanSaveSettings(object obj)
        {
            if (!(Settings is null)) return true;
            return false;
        }
        private void SelectImage(object obj)
        {

            Settings.Image = _windowService.ShowFileDialog();
        }
        private bool CanSelectImage(object obj)
        {
            return true;
        }        
        private void Closing(object obj)
        {
            if (!(Settings is null)) Messenger.Default.Send<SettingTextBoxes>(Settings);
            _windowService.ShowMainWindow();
        }
        private bool CanBeClosed(object obj)
        {
            return true;
        }

    }
}
