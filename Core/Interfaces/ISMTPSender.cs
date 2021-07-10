using System.Net.Mail;

namespace MyIssue.Core.Interfaces
{
    public interface ISMTPSender
    {
        void SendMessage(MailMessage message);
    }
}
