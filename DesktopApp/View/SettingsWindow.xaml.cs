using System;
using System.IO;
using System.Windows;
using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Files;
using MyIssue.DesktopApp.ViewModel;
using MyIssue.DesktopApp.Model;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyIssue.Core.Entities;

namespace MyIssue.DesktopApp.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private IImageFileDialog _dialog;
        private ITextBoxesToConfiguration _boxesToConfig;
        private bool IsSmtp { get; set; }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(Paths.path)) Directory.CreateDirectory(Paths.path);
            WriteConfig();
            if (!string.IsNullOrEmpty(imageLink.Text)) {
            } File.Copy(imageLink.Text, Paths.path + "image" + Path.GetExtension(imageLink.Text));
        }
        public SettingsWindow()
        {
            _boxesToConfig = new TextBoxesToConfiguration();
            _dialog = new ImageFileDialog();
            InitializeComponent();
        }
        private void WriteConfig()
        {

            _boxesToConfig.DoWriting(IsSmtp, (SettingTextBoxes)DataContext);
        }
        private void ApplicationPass_Changed(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(applicationPass.Text)) save.IsEnabled = true;
        }
        private void ImageSelect_Click(object sender, RoutedEventArgs e)
        {
            imageLink.Text = _dialog.ImageDialog();
        }
        private void SMTPChecked(object sender, RoutedEventArgs e)
        {
            IsSmtp = true;
            email.IsEnabled = true;
            recipient.IsEnabled = true;
            sslRadio.IsEnabled = true;
        }
        private void ServerChecked(object sender, RoutedEventArgs e)
        {
            IsSmtp = false;
            email.IsEnabled = false;
            recipient.IsEnabled = false;
            sslRadio.IsEnabled = false;
        }
    }
}
