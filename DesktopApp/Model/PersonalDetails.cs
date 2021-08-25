using System.ComponentModel;

namespace MyIssue.DesktopApp.Model
{
    public class PersonalDetails : INotifyPropertyChanged
    {
        private string _name;
        private string _surname;
        private string _company;
        private string _phone;
        private string _email;
        private string _image;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                RaisePropertyChanged("Surname");
            }
        }
        public string Company
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
                RaisePropertyChanged("Company");
            }
        }
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                RaisePropertyChanged("Phone");
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
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
