using System.ComponentModel;

namespace MyIssue.Core.Entities
{
    public class SettingTextBoxes : INotifyPropertyChanged
    {
        private string _applicationPass;
        private string _companyName;
        private string _serverAddress;
        private string _port;
        private string _login;
        private string _pass;
        private string _emailAddress;
        private string _recipientAddress;
        private string _connectionMethod;
        private string _sslTsl;
        private string _image;
        public string ApplicationPass
        {
            get
            {
                return _applicationPass;
            }
            set
            {
                _applicationPass = value;
                RaisePropertyChanged("ApplicationPass");
            }
        }
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
                RaisePropertyChanged("CompanyName");
            }
        }
        public string ServerAddress
        {
            get
            {
                return _serverAddress;
            }
            set
            {
                _serverAddress = value;
                RaisePropertyChanged("ServerAddress");
            }
        }
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                RaisePropertyChanged("Port");
            }
        }
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                RaisePropertyChanged("Login");
            }
        }
        public string Pass
        {
            get
            {
                return _pass;
            }
            set
            {
                _pass = value;
                RaisePropertyChanged("Pass");
            }
        }
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;
                RaisePropertyChanged("EmailAddress");
            }
        }
        public string RecipientAddress
        {
            get
            {
                return _recipientAddress;
            }
            set
            {
                _recipientAddress = value;
                RaisePropertyChanged("RecipientAddress");
            }
        }
        public string ConnectionMethod
        {
            get
            {
                return _connectionMethod;
            }
            set
            {
                _connectionMethod = value;
                RaisePropertyChanged("ConnectionMethod");
            }
        }
        public string SslTsl
        {
            get
            {
                return _sslTsl;
            }
            set
            {
                _sslTsl = value;
                RaisePropertyChanged("SslTsl");
            }
        }
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                RaisePropertyChanged("Image");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
