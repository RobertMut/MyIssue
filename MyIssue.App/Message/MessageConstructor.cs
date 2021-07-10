using System.Net.Mail;
using System;
using MyIssue.Core.Interfaces;
using MyIssue.Core.Entities;

namespace MyIssue.DesktopApp
{
    public class MessageConstructor : IMessageConstructor
    {
        public MailMessage BuildMessage(string subject,string recipient, string sender, PersonalDetails details, string description)
        {
            var formatted = string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}\r\n{5}",
                    details.Name,
                    details.Surname,
                    details.Company,
                    details.Phone,
                    details.Email,
                    description
                    );
            return new MailMessage(new MailAddress(sender), new MailAddress(recipient))
            {
                Subject = subject,
                Body = formatted,
                IsBodyHtml = false,
                };
        }
        public string BuildCommand(string subject, string description, DateTime date, string company, int importance)
        {
            return string.Format(ConsoleCommands.newTaskParameters, subject, description, date, company, importance);
        }
    }
}
