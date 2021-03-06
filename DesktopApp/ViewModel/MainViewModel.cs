using MyIssue.Core.Interfaces;
using MyIssue.DesktopApp.Misc;
using MyIssue.DesktopApp.Misc.Sender;
using MyIssue.Infrastructure.Files;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using MyIssue.DesktopApp.Model;

namespace MyIssue.DesktopApp.ViewModel
{
    public class MainViewModel : BindableBase, INavigationAware
    {
        private IUserData _loaduserdata;
        private ISelector _selector;
        private IRegionManager _regionManager;

        private PersonalDetails _details;
        private SettingTextBoxes _settings;
        private string _description;
        private bool _saveDetails;
        public DelegateCommand SendCommand { get; private set; }
        public DelegateCommand EditSettings { get; private set; }
        public DelegateCommand UserData { get; private set; }

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
            get 
            {
                return _settings; 
            }
            set
            {
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
            _loaduserdata = new UserData();
            _selector = new Selector();
            Settings = new SettingTextBoxes();
            Details = new PersonalDetails();
            LoadUserData();
        }

        private void LoadCommands()
        {
            EditSettings = new DelegateCommand(EnterSettings);
            SendCommand = new DelegateCommand(SendMessage);
            UserData = new DelegateCommand(LoadUserData);

        }
        private void EnterSettings()
        {
            var parameters = new NavigationParameters();
            parameters.Add("apppass", Settings.ApplicationPass);
            _regionManager.RequestNavigate("ContentRegion", "Prompt", Callback, parameters);
        }
        private void SendMessage()
        {
            List<string> details = new List<string>()
            {
                Details.Company,
                Details.Email,
                Details.Name,
                Details.Phone,
                Details.Surname,
                Description
            };
            if (details.Any(det => !string.IsNullOrEmpty(det)))
            {
                _selector.Send(Settings, Details, Description);
            }
            if (SaveDetails.Equals(true)) SavePersonal.Save(Details);

        }
        private void Callback(NavigationResult res)
        {
            if(!(res.Error is null))
            {
                SerilogLogger.ClientLogException(res.Error);
            }
        }
        private void LoadUserData()
        {
            try
            {
                Details = _loaduserdata.Load(Settings.Image, Settings.CompanyName);
            } catch (NullReferenceException e)
            {
                Details = null;
                SerilogLogger.ClientLogException(e);
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
