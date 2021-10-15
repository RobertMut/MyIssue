using System.Net.Mail;

namespace MyIssue.Infrastructure.Smtp
{
    public interface ISMTPSender
    {
        void SendMessage(MailMessage message);
    }
}
