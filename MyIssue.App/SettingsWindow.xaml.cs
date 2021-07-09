using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Linq;
using MyIssue.Server.IO;
using MyIssue.App.Cryptography;
using MyIssue.App.IO;
using System.Collections.ObjectModel;
using System.Reflection;

namespace MyIssue.App
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private bool isSmtp;
        private string templateLocation = string.Empty;
        private string imageLocation = string.Empty;
        private readonly IWriteConfig _write;
        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(Paths.path)) Directory.CreateDirectory(Paths.path);
            WriteConfig();
            if (!string.IsNullOrEmpty(imageLocation)) {
            } File.Copy(imageLocation, Paths.path + "image" + Path.GetExtension(imageLocation));
        }
        public SettingsWindow()
        {
            _write = new WriteConfiguration();
            InitializeComponent();
        }
        private void WriteConfig()
        {

            if (File.Exists(Paths.confFile)) File.Delete(Paths.confFile);
            switch (isSmtp)
            {
                case true:
                    Stream emailConf = Assembly.GetExecutingAssembly().GetManifestResourceStream("DesktopApp.Files.configuration.xml");
                    _write.WriteEmptyConfig(Paths.confFile,
                        string.Format(LoadFile.Load(emailConf),
                        Crypto.AesEncrypt(applicationPass.Text),
                        companyName.Text,
                        Crypto.AesEncrypt(smtp.Text, applicationPass.Text),
                        Crypto.AesEncrypt(port.Text, applicationPass.Text),
                        Crypto.AesEncrypt(login.Text, applicationPass.Text),
                        Crypto.AesEncrypt(password.Password, applicationPass.Text),
                        Crypto.AesEncrypt(email.Text, applicationPass.Text),
                        Crypto.AesEncrypt(recipient.Text, applicationPass.Text),
                        isSmtp.ToString(),
                        sslRadio.IsChecked,
                        imageLink.Text
                        ));
                    break;
                case false:
                    Stream serverConf = Assembly.GetExecutingAssembly().GetManifestResourceStream("DesktopApp.Files.configurationServer.xml");
                    _write.WriteEmptyConfig(Paths.confFile,
                        string.Format(LoadFile.Load(serverConf),
                        Crypto.AesEncrypt(applicationPass.Text),
                        companyName.Text,
                        Crypto.AesEncrypt(server.Text, applicationPass.Text),
                        Crypto.AesEncrypt(serverport.Text, applicationPass.Text),
                        Crypto.AesEncrypt(serverlogin.Text, applicationPass.Text),
                        Crypto.AesEncrypt(serverpass.Password, applicationPass.Text),
                        isSmtp.ToString(),
                        imageLink.Text
                        ));
                    break;
            }
        }
        private void ApplicationPass_Changed(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(applicationPass.Text)) save.IsEnabled = true;
        }
        private void TemplateSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                templateLink.Text = fileDialog.FileName;
                templateLocation = templateLink.Text;
            }
        }
        private void ImageSelect_Click(object sender, RoutedEventArgs e)
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
            isSmtp = true;
            serverport.IsEnabled = false;
            serverpass.IsEnabled = false;
            serverlogin.IsEnabled = false;
            server.IsEnabled = false;
            smtp.IsEnabled = true;
            port.IsEnabled = true;
            login.IsEnabled = true;
            password.IsEnabled = true;
            email.IsEnabled = true;
            recipient.IsEnabled = true;
            sslRadio.IsEnabled = true;
        }
        private void ServerChecked(object sender, RoutedEventArgs e)
        {
            isSmtp = false;
            serverport.IsEnabled = true;
            serverpass.IsEnabled = true;
            serverlogin.IsEnabled = true;
            server.IsEnabled = true;
            smtp.IsEnabled = false;
            port.IsEnabled = false;
            login.IsEnabled = false;
            password.IsEnabled = false;
            email.IsEnabled = false;
            recipient.IsEnabled = false;
            sslRadio.IsEnabled = false;
        }
    }
}
