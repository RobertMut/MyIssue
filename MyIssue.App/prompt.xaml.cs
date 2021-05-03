using System.Windows;

namespace MyIssue.App
{

    public partial class Prompt : Window
    {
        
        //IO iO = new IO();
        public Prompt()
        {
                InitializeComponent();


        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

            //shit went wrong!!
            if (passwordBox.Text.Equals(
                        Crypto.AesDecrypt(IO.encryptedData[0], IO.encryptedData[1]))) 
            {
                SettingsWindow settings = new SettingsWindow();
                settings.Show();
                info.Content = "Correct password!";
                this.Close();
            } else
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
