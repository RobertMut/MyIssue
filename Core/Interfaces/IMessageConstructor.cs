using MyIssue.Core.Entities;
using System.Collections.Generic;
using System.Net.Mail;

namespace MyIssue.Core.Interfaces
{
    public interface IMessageConstructor
    {
        MailMessage BuildMessage(string subject, string recipient, string sender, PersonalDetails details, string description);
        IEnumerable<string> BuildTaskCommands(SettingTextBoxes settings, string description);
    }
}