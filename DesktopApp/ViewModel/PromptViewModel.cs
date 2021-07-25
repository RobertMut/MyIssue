using MyIssue.DesktopApp.Model.Services;
using MyIssue.DesktopApp.Model.Utility;
using System.ComponentModel;
using System.Windows.Input;

namespace MyIssue.DesktopApp.ViewModel
{
    public class PromptViewModel : INotifyPropertyChanged
    {
        private string enteredPassword;
        private string labelText;
        private string ApplicationPass { get; set; }
        public ICommand OpenSettings { get; set; }
        public ICommand ReturnToMainView { get; set; }
        private IWindowService _windowService;
        public event PropertyChangedEventHandler PropertyChanged;
        public string LabelText
        {
            get
            {
                return labelText;
            }
            private set
            {
                labelText = value;
                RaisePropertyChanged("LabelText");
            }
        }
        public string EnteredPassword
        {
            get
            {
                return enteredPassword;
            }
            set
            {
                enteredPassword = value;
                RaisePropertyChanged("EnteredPassword");
            }
        }
        public PromptViewModel(IWindowService service)
        {
            _windowService = service;
            LoadCommands();
            Messenger.Default.Register<string>(this, OnPasswordReceived);
        }
        public void RaisePropertyChanged(string property)
        {
            if (!(PropertyChanged is null)) PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void LoadCommands()
        {
            OpenSettings = new CustomCommands(OpenSettingsButton, CanOpenSettingsButton);
            ReturnToMainView = new CustomCommands(GoBack, CanGoBack);
        }
        private void OnPasswordReceived(string pass)
        {
            ApplicationPass = pass;
        }
        private void OpenSettingsButton(object obj)
        {
            _windowService.ShowSettings();
        }
        private bool CanOpenSettingsButton(object obj)
        {
            if (ApplicationPass.Equals(enteredPassword)) return true;
            return false;
        }
        private void GoBack(object obj)
        {
            _windowService.ShowMainWindow();
        }
        private bool CanGoBack(object obj)
        {
            return true;
        }
    }
}
