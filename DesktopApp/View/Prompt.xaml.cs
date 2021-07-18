using System.Windows;
using MyIssue.DesktopApp.Model;

namespace MyIssue.DesktopApp.View
{
    public partial class Prompt : Window
    {
        public Prompt()
        {
            InitializeComponent();

        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Text.Equals(DesktopConfig.Config.ApplicationPass))
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
