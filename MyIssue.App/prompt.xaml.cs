using System.Windows;
using MyIssue.App.IO;

namespace MyIssue.App
{
    public partial class Prompt : Window
    {
        private readonly IDecryptedValue _decrypt;
        public Prompt()
        {
            InitializeComponent();
            _decrypt = new DecryptedValue(Paths.confFile);

        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Text.Equals(_decrypt.GetValue("applicationPass")))
            {
                new SettingsWindow().Show();
                info.Content = "Correct password!";
                this.Close();
            }
            else
            {
                info.Content = "Incorrect password!";
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Close();
            this.Close();
        }
    }
}
