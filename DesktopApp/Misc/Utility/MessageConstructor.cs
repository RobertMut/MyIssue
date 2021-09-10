using System.Net.Mail;
using System;
using MyIssue.Core.Interfaces;
using System.Collections.Generic;
using MyIssue.Core.String;
using MyIssue.Core.Constants;
using MyIssue.DesktopApp.Model;

namespace MyIssue.DesktopApp.Misc.Utility
{
    public class MessageConstructor : IMessageConstructor
    {
        public MailMessage BuildMessage(string subject, string recipient, string sender, PersonalDetails details, string description)
        {
            var newsubj = $"[Issue][{details.Company}][{details.Name}][{details.Surname}][{subject}]";
            var formatted =
                $"{details.Name}\r\n{details.Surname}\r\n{details.Company}\r\n{details.Phone}\r\n{details.Email}\r\n{description}";
            return new MailMessage(sender, recipient, newsubj, formatted);
        }
        public IEnumerable<string> BuildTaskCommands(SettingTextBoxes settings, string description)
        {
            return new List<string>()
            {
                ConsoleCommands.login,
                string.Format(ConsoleCommands.loginParameters, settings.Login, settings.Pass),
                ConsoleCommands.newTask,
                string.Format(ConsoleCommands.newTaskParameters, StringStatic.CutString(description), description, DateTime.Now, settings.CompanyName, 1),
                ConsoleCommands.logout
            };
        }
    }
}

