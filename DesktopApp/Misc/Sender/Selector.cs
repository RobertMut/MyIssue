using MyIssue.Core.Interfaces;
using MyIssue.Core.String;
using MyIssue.DesktopApp.Misc.Utility;
using MyIssue.Infrastructure.Files;
using System;
using MyIssue.DesktopApp.Model;
using MyIssue.Infrastructure.Model;
using MyIssue.Infrastructure.Smtp;

namespace MyIssue.DesktopApp.Misc.Sender
{
    public class Selector : ISelector
    {
        private IMessageConstructor _message;
        private ISMTPSender _sender;
        private IConsoleClient _client;
        public Selector()
        {
            _message = new MessageConstructor();
           
        }
        public void Send(SettingTextBoxes settings, PersonalDetails details, string description)
        {
            if (settings.ConnectionMethod.Equals("True"))
            {
                var m = _message.BuildMessage(StringStatic.CutString(description),
                settings.RecipientAddress, settings.EmailAddress, details, description);
                _sender = new SMTPSender(new SettingTextBoxesForSmtp
                {
                    ServerAddress = settings.ServerAddress,
                    Port = settings.Port,
                    Login = settings.Login,
                    Pass = settings.Pass,
                    SslTsl = settings.SslTsl

                });
                _sender.SendMessage(m);
            }
            else
            {
                try
                {
                    var m = _message.BuildTaskCommands(settings, description);
                    _client = new ConsoleClient(settings, m);
                    _client.Client();
                }
                catch (Exception e)
                {
                    SerilogLogger.ClientLogException(e);
                }

            }
        }
    }
}
