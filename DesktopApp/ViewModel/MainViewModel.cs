using MyIssue.Core.Entities;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using MyIssue.DesktopApp.Misc;
using MyIssue.DesktopApp.Misc.Services;
using MyIssue.DesktopApp.Misc.Sender;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace MyIssue.DesktopApp.ViewModel
{
    public class MainViewModel : BindableBase
    {

        private IDesktopData _data;
        private IUserData _loaduserdata;
        private ISelector _selector;
        private IRegionManager _regionManager;

        private PersonalDetails _details;
        private SettingTextBoxes _settings;
        private string _description;
        private bool _saveDetails;
        private bool canExecute = true;
        public DelegateCommand SendCommand { get; private set; }
        public DelegateCommand EditSettings { get; private set; }
        public DelegateCommand UserData { get; private set; }

        public bool CanExecute
        {
            get { return canExecute; }
            set
            {
                SetProperty(ref canExecute, value);
            }
        }
        public bool SaveDetails
        {
            get { return _saveDetails; }
            set
            {
                SetProperty(ref _saveDetails, value);
            }
        }
        public PersonalDetails Details
        {
            get { return _details; }
            set
            {
                SetProperty(ref _details, value);
            }
        }
        public SettingTextBoxes Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                SetProperty(ref _settings, value);
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                SetProperty(ref _description, value);
            }
        }
        public MainViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoadCommands();
            _data = new DesktopData();
            _loaduserdata = new UserData();
            _selector = new Selector();
            Settings = new SettingTextBoxes();
            Details = new PersonalDetails();
            LoadData();
            LoadUserData();
        }

        private void LoadCommands()
        {
            EditSettings = new DelegateCommand(EnterSettings, CanEnterSettings);
            SendCommand = new DelegateCommand(SendMessage, CanDoOtherThings);
            UserData = new DelegateCommand(LoadUserData, CanDoOtherThings);

        }
        private void LoadData()
        {
            try
            {
                Settings = _data.Load();
            } catch (ConfigurationNotFoundException e)
            {
                canExecute = false;
                _regionManager.RequestNavigate("ContentRegion", "SettingsView", Callback);
                SerilogLoggerService.LogException(e);
            }
        }
        private void EnterSettings()
        {
            var parameters = new NavigationParameters();
            parameters.Add("apppass", Settings.ApplicationPass);
            _regionManager.RequestNavigate("ContentRegion", "Prompt", Callback, parameters);
        }
        private bool CanEnterSettings()
        {
            return true;
        }
        private void SendMessage()
        {
            _selector.Send(Settings, Details, Description); //TODO: check if null
            if (SaveDetails.Equals(true)) SavePersonal.Save(Details);
        }
        private bool CanDoOtherThings()
        {
            return true;
        }
        private void Callback(NavigationResult res)
        {
            if(!(res.Error is null))
            {
                SerilogLoggerService.LogException(res.Error);
            }
        }
        private void LoadUserData()
        {
            try
            {
                if (canExecute)
                Details = _loaduserdata.Load(Settings.Image, Settings.CompanyName);
            } catch (NullReferenceException e)
            {
                SerilogLoggerService.LogException(e);
            }

        }
    }
}
