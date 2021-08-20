using System;
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
                NotifyPropertyChanged("ApplicationPass");
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
                NotifyPropertyChanged("CompanyName");
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
                NotifyPropertyChanged("ServerAddress");
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
                NotifyPropertyChanged("Port");
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
                NotifyPropertyChanged("Login");
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
                NotifyPropertyChanged("Pass");
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
                NotifyPropertyChanged("EmaiilAddress");
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
                NotifyPropertyChanged("RecipientAddress");
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
                NotifyPropertyChanged("ConnectionMethod");
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
                NotifyPropertyChanged("SslTsl");
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
                NotifyPropertyChanged("Image");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
