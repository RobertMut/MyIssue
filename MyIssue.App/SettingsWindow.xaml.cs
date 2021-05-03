using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace MyIssue.App
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private string templateLocation = "Empty";
        private string imageLocation = "Empty";
        //private bool errorFlag;
        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            string aPass = applicationPass.Text;
            templateLocation = templateLink.Text;
            imageLocation = imageLink.Text;
            templateLocation = CopyFile(templateLocation, "template.html");
            imageLocation = CopyFile(imageLocation, "image"+Path.GetExtension(imageLocation));
            //Convert can deal with null
            string bName = Convert.ToString(buisnessName.Text);
            string[] settings =
            {
                server.Text, port.Text,
                login.Text, password.Password,
                mail.Text, recipient.Text,
                
                bName, templateLocation, imageLocation
            };
            bool[] selectedEncryption =
            {
                (bool)smtpRadio.IsChecked, (bool)sslRadio.IsChecked, !String.IsNullOrEmpty(buisnessName.Text)
            };
            using (IO iO = new IO())
            {
                iO.ConfigurationCreator(aPass, settings, selectedEncryption);
            }
            
        }
        public SettingsWindow()
        {
            //bool error = false) errorFlag = error;
            InitializeComponent();
        }
        private static string CopyFile(string str, string filename)
        {
            Regex regex = new Regex(@"\S\:\\.*");
            if (regex.IsMatch(str))
            {
                if (File.Exists(MainWindow.path + filename)) File.Delete(MainWindow.path + filename);
                File.Copy(str, MainWindow.path + filename);
                Debug.WriteLine("Copied");
                return MainWindow.path + filename;
            } else
            {
                Debug.WriteLine("NOT MATCH");
                return str;
            }
        }
        private void ApplicationPass_Changed(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(applicationPass.Text)) save.IsEnabled = true;
        }
        private void templateSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == true)
            {
                templateLink.Text = fileDialog.FileName;
                templateLocation = templateLink.Text;
            }
        }
        private void imageSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                imageLink.Text = fileDialog.FileName;
                imageLocation = imageLink.Text;
            }
        }
        private void SMTPChecked(object sender, RoutedEventArgs e)
        {
            standServer.IsEnabled = false;
            standPort.IsEnabled = false;
            passphrase.IsEnabled = false;
            server.IsEnabled = true;
            port.IsEnabled = true;
            login.IsEnabled = true;
            password.IsEnabled = true;
            mail.IsEnabled = true;
            recipient.IsEnabled = true;
            sslRadio.IsEnabled = true;
        }
        private void ServerChecked(object sender, RoutedEventArgs e)
        {
            standServer.IsEnabled = true;
            standPort.IsEnabled = true;
            passphrase.IsEnabled = true;
            server.IsEnabled = false;
            port.IsEnabled = false;
            login.IsEnabled = false;
            password.IsEnabled = false;
            mail.IsEnabled = false;
            recipient.IsEnabled = false;
            sslRadio.IsEnabled = false;
        }
    }
}
