using MyIssue.Core.Entities;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using MyIssue.DesktopApp.Model;
using MyIssue.DesktopApp.Model.Exceptions;
using MyIssue.DesktopApp.Model.Services;
using MyIssue.DesktopApp.Model.Utility;
using System.ComponentModel;
using System.Windows.Input;

namespace MyIssue.DesktopApp.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IDesktopData _data;
        private IUserData _loaduserdata;
        private PersonalDetails details;
        private SettingTextBoxes settings;
        private string description;
        private IWindowService _windowService;
        private IDesktopExceptionHandler _exceptionHandler;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SendCommand { get; set; }
        public ICommand EditSettings { get; set; }
        public ICommand UserData { get; set; }
        public bool IsDoingOtherThings
        {
            get { return isDoingOtherThings; }
            set
            {
                isDoingOtherThings = value;
                RaisePropertyChanged("IsDoingOtherThings");
            }
        }
        public PersonalDetails Details
        {
            get { return details; }
            set
            {
                details = value;
                RaisePropertyChanged("Details");
            }
        }
        public SettingTextBoxes Settings
        {
            get { return settings; }
            set
            {
                settings = value;
                RaisePropertyChanged("Settings");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged("Description");
            }
        }
        private bool isDoingOtherThings;
        public MainWindowViewModel(IWindowService service)
        {
            _exceptionHandler = new DesktopExceptionHandler();
            _data = new DesktopData();
            _loaduserdata = new UserData();
            _windowService = service;
            isDoingOtherThings = false;
                LoadData();
                LoadCommands();
        }

        private void LoadCommands()
        {
            EditSettings = new CustomCommands(EnterSettings, CanEnterSettings);
            SendCommand = new CustomCommands(SendMessage, CanDoOtherThings);
            UserData = new CustomCommands(LoadUserData, CanDoOtherThings);
        }
        private void RaisePropertyChanged(string property)
        {
            if (!(PropertyChanged is null)) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        private void LoadData()
        {
            try
            {
                Settings = _data.Load();
            } catch (ConfigurationNotFoundException e)
            {
                IsDoingOtherThings = true;
                _windowService.ShowSettings();
                _exceptionHandler.HandleExceptions(e);
            }
        }
        private void EnterSettings(object obj)
        {
            IsDoingOtherThings = true;
            Messenger.Default.Send(Settings.ApplicationPass);

            _windowService.ShowPrompt();
        }
        private bool CanEnterSettings(object obj)
        {
            if (IsDoingOtherThings) return false;
            return true;
        }
        private void SendMessage(object obj)
        {
            IsDoingOtherThings = true;
            Messenger.Default.Send(IsDoingOtherThings);
            //TODO: SEND MESSAGE
        }
        private bool CanDoOtherThings(object obj)
        {
            if (IsDoingOtherThings) return false;
            return true;
        }
        private void LoadUserData(object obj)
        {
            details = _loaduserdata.Load(Settings.Image, Settings.CompanyName);
        }
    }
}
