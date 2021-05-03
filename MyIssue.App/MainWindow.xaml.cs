using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyIssue.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<string> input = new List<string>();
        public static string path = Environment.ExpandEnvironmentVariables(@"%APPDATA%\SimpleIssueReporting\");
        public static string confFile = Environment.ExpandEnvironmentVariables(@"%APPDATA%\SimpleIssueReporting\configuration.xml");
        public static string userFile = Environment.ExpandEnvironmentVariables(@"%APPDATA%\SimpleIssueReporting\userData.xml");
        public MainWindow()
        {
            //TODO: checker if i created a new config or if config doesnt exist
            Debug.WriteLine("ConfFlag equals {0}", IO.configurationFlag);
            InitializeComponent();
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            Info.Text = "Sending data....";
            input.Add(name.Text);
            input.Add(surname.Text);
            input.Add(company.Text);
            input.Add(phoneNumber.Text);
            input.Add(email.Text);
            input.Add(desc.Text);
            bool toSaveBox = !(bool)saveData.IsChecked;
            switch (toSaveBox)
            {
                case true:
                    MessageBoxResult saveMessage = MessageBox.Show("Do you want to save your information?", "Save Information",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (saveMessage.Equals(MessageBoxResult.Yes)) {
                        using (IO iO = new IO())
                        {
                            iO.FormWriter(toSaveBox, path, userFile);
                        }
                        
                    }
                    break;
            }
            try
            {
                Crypto crypto = new Crypto();
                crypto.DecryptData();

                ConnectionMethod();
                Info.Text = "Data Successfully send!";
            } catch (Exception ex)
            {
                Debug.WriteLine("Sending message exception -> {0}", ex);
                Info.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                Info.Text = "ERROR OCCURED!";
            }

        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (IO.configurationFlag.Equals(false))
            {
                Prompt prompt = new Prompt();
                prompt.Show();
            } else
            {
                MessageBox.Show("Something went wrong. Opening configuration menu..", "Critical error!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                SettingsWindow settings = new SettingsWindow();
                settings.Show();
            }

        }
        private void LoadConfig(object sender, RoutedEventArgs e)
        {
            string[] values;
            using (IO iO = new IO())
            {
                IO.encryptedData = iO.ConfigurationReader();
                values = iO.FormReader();
            }
            if (values.Length == 4)
            {
                name.Text = values[0];
                surname.Text = values[1];
                email.Text = values[2];
                saveData.IsChecked = Convert.ToBoolean(values[3]);

            }
            if (IO.configurationFlag.Equals(false)) company.Text = IO.encryptedData[1];
            if (IO.configurationFlag.Equals(false)) Image();


        }
        private void Image()
        {
            if (!IO.encryptedData[11].Equals("Empty"))
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(IO.encryptedData[12]);
                img.EndInit();
                image.Source = img;
            }

        }
        private void ConnectionMethod()
        {
            Debug.WriteLine(IO.encryptedData.Count - 5);
            Debug.WriteLine(IO.encryptedData.Count - 4);
            bool conn = IO.encryptedData[IO.encryptedData.Count - 5] == "True";
            bool ssl = IO.encryptedData[IO.encryptedData.Count - 4] == "True";
            using (SMTPCli sMTP = new SMTPCli())
            using(ServerCli cli = new ServerCli())
            {
                if (conn.Equals(true) && ssl.Equals(false))
                {

                    sMTP.TelnetClient();
                }
                else if (conn.Equals(true) && ssl.Equals(true))
                {
                    sMTP.SSLClient();
                } else
                {
                    cli.TcpClient();
                }
            }
                
        }
        }
    }