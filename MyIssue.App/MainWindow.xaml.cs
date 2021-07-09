using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MyIssue.Server.IO;
using MyIssue.App.IO;
using System.IO;
using MyIssue.App.Personal;
using MyIssue.App.SMTP;
using System.Xml.Linq;
using System.Reflection;

namespace MyIssue.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IWriteConfig _write;
        private readonly IMessageConstructor _message;
        private readonly IDecryptedValue _dval;
        private readonly IReadConfig _read;
        private readonly ISMTPSender _smtp;
        private readonly string applicationPassword;
        private readonly XDocument configuration;
        private bool isCreated = false;
        public MainWindow()
        {
            if (!File.Exists(Paths.confFile))
            {
                MessageBox.Show("Write new configuration and restart application.", "Critical error!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                new SettingsWindow().Show();
            }
            else
            {
                isCreated = true;
                _write = new WriteConfiguration();
                _message = new MessageConstructor();
                _dval = new DecryptedValue(Paths.confFile);
                applicationPassword = _dval.GetValue("applicationPass");
                _read = new OpenConfiguration();
                configuration = _read.OpenConfig(Paths.confFile);
                this.DataContext = new View(ConfigValue.GetValue("image", configuration));
                _smtp = new SMTPSender(applicationPassword);
                //Image();
            }
            InitializeComponent();
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            Info.Text = "Sending data....";
            if (ConfigValue.GetValue("isSmtp", configuration).Equals("True")) SendEmail();
            else SendThroughServer();

            bool toSaveBox = !(bool)saveData.IsChecked;
            switch (toSaveBox)
            {
                case true:
                    MessageBoxResult saveMessage = MessageBox.Show("Do you want to save your personal details?", "Save details",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (saveMessage.Equals(MessageBoxResult.Yes))
                    {
                        var userData = LoadFile.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("DesktopApp.Files.userData.xml"));
                        if (File.Exists(Paths.userFile)) File.Delete(Paths.userFile);
                        _write.WriteEmptyConfig(Paths.userFile,
                            string.Format(userData,
                            name.Text,
                            surname.Text,
                            email.Text,
                            phoneNumber.Text
                            ));
                    }
                    break;
            }
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            new Prompt().Show();

        }
        private void SendEmail()
        {
            var details = DetailsBuilder.Create()
               .SetName(name.Text)
               .SetSurname(surname.Text)
               .SetCompany(company.Text)
               .SetPhone(phoneNumber.Text)
               .SetEmail(email.Text)
               .Build();
            var subject = string.Format(
                "[Issue][{0}][{1}][{2}][{3}]",
                ConfigValue.GetValue("companyName", configuration),
                name.Text,
                surname.Text,
                desc.Text.Substring(0, Math.Min(desc.Text.Length / 3, 15))
            );
            var recipient = _dval.GetValue("recipientAddress", applicationPassword);
            var sender = _dval.GetValue("emailAddress", applicationPassword);
            var msg = _message.BuildMessage(
                subject,
                recipient, //recipient
                sender,
                details, //details
                desc.Text //desc
                );
            _smtp.SendMessage(msg);
        }
        private void SendThroughServer()
        {
            var cmd = _message.BuildCommand(desc.Text.Substring(0, Math.Min(desc.Text.Length/3, 15)), desc.Text, DateTime.Now, company.Text, 1);
            new ConsoleClient(
                _dval.GetValue("serverAddress", applicationPassword),
                Int32.Parse(_dval.GetValue("port", applicationPassword)),
                _dval.GetValue("login", applicationPassword),
                _dval.GetValue("pass", applicationPassword)).Client(cmd);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (!isCreated)
            {
                name.IsEnabled = false;
                surname.IsEnabled = false;
                company.IsEnabled = false;
                phoneNumber.IsEnabled = false;
                email.IsEnabled = false;
                desc.IsEnabled = false;
                sendButton.IsEnabled = false;
            }
            else company.Text = ConfigValue.GetValue("companyName", configuration);
            if (File.Exists(Paths.userFile))
            {
                var userFile = _read.OpenConfig(Paths.userFile);
                name.Text = ConfigValue.GetValue("name", userFile);
                surname.Text = ConfigValue.GetValue("surname", userFile);
                email.Text = ConfigValue.GetValue("email", userFile);
                phoneNumber.Text = ConfigValue.GetValue("phone", userFile);
            }
        }
    }
}