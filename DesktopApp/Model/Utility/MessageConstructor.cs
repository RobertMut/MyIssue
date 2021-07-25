using System.Net.Mail;
using System;
using MyIssue.Core.Interfaces;
using MyIssue.Core.Entities;
using System.Collections.Generic;
using MyIssue.Core.String;

namespace MyIssue.DesktopApp.Model.Utility
{
    public class MessageConstructor : IMessageConstructor
    {
        public MailMessage BuildMessage(string subject, string recipient, string sender, PersonalDetails details, string description)
        {
            var newsubj = string.Format("[Issue][{0}][{1}][{2}][{3}]", details.Company, details.Name, details.Surname, subject);
            var formatted = string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{5}",
                    details.Name,
                    details.Surname,
                    details.Company,
                    details.Phone,
                    details.Email,
                    description
                    );
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

