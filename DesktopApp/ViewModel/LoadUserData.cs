using MyIssue.DesktopApp.Model;
using System;
using System.Windows.Media.Imaging;

namespace MyIssue.DesktopApp.ViewModel
{
    public class DataProperties
    {
        public string Name
        {
            get;
            set;
        } = UserDetails.details.Name;
        public string Surname
        {
            get;
            set;
        } = UserDetails.details.Surname;
        public string Email
        {
            get;
            set;
        } = UserDetails.details.Email;
        public string Phone
        {
            get;
            set;
        } = UserDetails.details.Phone;
        public string Company
        {
            get;
            set;
        } = DesktopConfig.Config.CompanyName;
        public BitmapImage Image
        {
            get;
            set;
        } = new BitmapImage(new Uri(DesktopConfig.Config.Image));
        public string Description
        {
            get;
            set;
        }
    }
}
