using System.Windows;

namespace MyIssue.DesktopApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private readonly ISaveMessageBox _box;
        //private readonly IMessageConstructor _message;
        //private readonly ISMTPSender _smtp;
        //private readonly IDesktopData _loadconfigdata;
        //private readonly IUserData _loaduserdata;
        //private readonly IExceptionMessageBox _exceptionMessage;
        //public DataProperties UserProperties { get; set; }
        public MainWindow()
        {
            //try
            //{
            //    _loadconfigdata = new DesktopData();
            //    _loadconfigdata.Load();
            //    _loaduserdata = new UserData();
            //    _loaduserdata.Load();
            //    UserProperties = new DataProperties();
            //    _box = new SaveMessageBox();
            //    _message = new MessageConstructor();
            //    _exceptionMessage = new ExceptionMessageBox();
            //    _smtp = new SMTPSender(DesktopConfig.Config);
                
            //}
            //catch (ConfigurationNotFoundException e)
            //{
            //    _exceptionMessage.ShowException(e);
            //    new SettingsWindow().Show();
            //}
            InitializeComponent();
        }

    
        //private void SendButton_Click(object sender, RoutedEventArgs e)
        //{
            //Info.Text = "Sending data....";
            //if (DesktopConfig.Config.ConnectionMethod.Equals("True")) SendEmail();
            //else SendThroughServer();
            //if(!(bool)saveData.IsChecked) _box.Box(UserProperties.Name, UserProperties.Surname, UserProperties.Email, UserProperties.Phone);
            //Info.Text = "Data sent";
        //}
        //private void SettingsButton_Click(object sender, RoutedEventArgs e)
        //{
            //new Prompt().Show();

        //}
        //private void SendEmail()
        //{
            //var details = PersonalDetailsBuilder.Create()
            //   .SetName(UserProperties.Name)
            //   .SetSurname(UserProperties.Surname)
            //   .SetCompany(UserProperties.Company)
            //   .SetPhone(UserProperties.Phone)
            //   .SetEmail(UserProperties.Email)
            //   .Build();
            //var subject = string.Format(
            //    "[Issue][{0}][{1}][{2}][{3}]",
            //    UserProperties.Company,
            //    UserProperties.Name,
            //    UserProperties.Surname,
            //    StringStatic.CutString(desc.Text)
            //);
            //var msg = _message.BuildMessage(
            //    subject,
            //    DesktopConfig.Config.RecipientAddress,
            //    DesktopConfig.Config.EmailAddress,
            //    details,
            //    desc.Text
            //    );
            //_smtp.SendMessage(msg);
        //}
        //private void SendThroughServer()
        //{
            //var cmd = _message.BuildCommand(StringStatic.CutString(desc.Text), desc.Text, DateTime.Now, company.Text, 1);
            //new ConsoleClient(
            //    DesktopConfig.Config.ServerAddress,
            //    Int32.Parse(DesktopConfig.Config.Port),
            //    DesktopConfig.Config.Login,
            //    DesktopConfig.Config.Pass).Client(cmd);
            
        //}
    }
}