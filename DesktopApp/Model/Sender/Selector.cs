using MyIssue.Core.Entities;
using MyIssue.Core.Interfaces;
using MyIssue.Core.String;
using MyIssue.DesktopApp.Model.Exceptions;
using MyIssue.DesktopApp.Model.Utility;
using MyIssue.Infrastructure.SMTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.DesktopApp.Model.Sender
{
    class Selector : ISelector
    {
        private IMessageConstructor _message;
        private ISMTPSender _sender;
        private IDesktopExceptionHandler _exceptionHandler;
        private IConsoleClient _client;
        public Selector()
        {
            _message = new MessageConstructor();
            _exceptionHandler = new DesktopExceptionHandler();
        }
        public void Send(SettingTextBoxes settings, PersonalDetails details, string description)
        {
            if (settings.ConnectionMethod.Equals(true))
            {
                var m = _message.BuildMessage(StringStatic.CutString(description),
                settings.RecipientAddress, settings.EmailAddress, details, description);
                _sender = new SMTPSender(settings);
                _sender.SendMessage(m);
            }
            else
            {
                try
                {
                    var m = _message.BuildTaskCommands(settings, description);
                    _client = new ConsoleClient(settings, _exceptionHandler, m);
                    _client.Client();
                }
                catch (Exception e)
                {
                    _exceptionHandler.HandleExceptions(e);
                }

            }
        }
    }
}
