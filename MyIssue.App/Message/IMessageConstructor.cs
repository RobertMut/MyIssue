using MyIssue.App.Personal;
using System;
using System.Net.Mail;

namespace MyIssue.App
{
    public interface IMessageConstructor
    {
        MailMessage BuildMessage(string subject, string recipient, string sender, PersonalDetails details, string description);
        string BuildCommand(string subject, string description, DateTime date, string company, int importance);
    }
}