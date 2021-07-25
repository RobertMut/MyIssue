using MyIssue.Core.Entities;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using MyIssue.DesktopApp.Model;
using MyIssue.DesktopApp.Model.Exceptions;
using MyIssue.DesktopApp.Model.Sender;
using MyIssue.DesktopApp.Model.Services;
using MyIssue.DesktopApp.Model.Utility;
using MyIssue.DesktopApp.ViewModel.Converters;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace MyIssue.DesktopApp.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IDesktopData _data;
        private IUserData _loaduserdata;
        private IWindowService _windowService;
        private IDesktopExceptionHandler _exceptionHandler;
        private ISelector _selector;

        private PersonalDetails details;
        private SettingTextBoxes settings;
        private string description;
        private bool saveDetails;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SendCommand { get; set; }
        public ICommand EditSettings { get; set; }
        public ICommand UserData { get; set; }
        public IValueConverter localImageConverter { get; set; }
        public bool SaveDetails
        {
            get { return saveDetails; }
            set
            {
                saveDetails = value;
                RaisePropertyChanged("SaveDetails");
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

        public MainWindowViewModel(IWindowService service)
        {
            _exceptionHandler = new DesktopExceptionHandler();
            _data = new DesktopData();
            _loaduserdata = new UserData();
            _windowService = service;
            localImageConverter = new ImageConverter();
            _selector = new Selector();
            LoadData();
            LoadCommands();
            Messenger.Default.Register<SettingTextBoxes>(this, OnSettingTextBoxesReceived);
        }

        private void OnSettingTextBoxesReceived(SettingTextBoxes obj)
        {
            Settings = obj;
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
                _windowService.ShowSettings();
                _exceptionHandler.HandleExceptions(e);
            }
        }
        private void EnterSettings(object obj)
        {
            Messenger.Default.Send<string>(Settings.ApplicationPass);

            _windowService.ShowPrompt();
        }
        private bool CanEnterSettings(object obj)
        {
            return true;
        }
        private void SendMessage(object obj)
        {
            _selector.Send(Settings, Details, Description);
            if (SaveDetails.Equals(true)) SavePersonal.Save(Details);
        }
        private bool CanDoOtherThings(object obj)
        {
            return true;
        }
        private void LoadUserData(object obj)
        {
            details = _loaduserdata.Load(Settings.Image, Settings.CompanyName);
        }
    }
}
