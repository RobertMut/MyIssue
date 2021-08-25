using System.Collections.Generic;
using System.Net.Mail;
using MyIssue.DesktopApp.Model;

namespace MyIssue.DesktopApp.Misc.Utility
{
    public interface IMessageConstructor
    {
        MailMessage BuildMessage(string subject, string recipient, string sender, PersonalDetails details, string description);
        IEnumerable<string> BuildTaskCommands(SettingTextBoxes settings, string description);
    }
}