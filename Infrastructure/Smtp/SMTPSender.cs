using System;
using System.Net.Mail;
using System.Net;
using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Files;
using System.Xml.Linq;
using MyIssue.Core.Entities;

namespace MyIssue.DesktopApp.SMTP
{
    public class SMTPSender : ISMTPSender
    {
        private readonly SmtpClient client;
        private readonly string pass;
        private readonly string login;
        private readonly bool sslTsl;

        public SMTPSender(SettingTextBoxes config)
        {

            client = new SmtpClient(config.ServerAddress, 
                Int32.Parse(config.Port));
            login = config.Login;
            pass = config.Pass;
            sslTsl = Convert.ToBoolean(config.SslTsl);
            
        }
        public void SendMessage(MailMessage message)
        {
            client.Credentials = new NetworkCredential(login, pass);
            client.EnableSsl = Convert.ToBoolean(sslTsl);
            client.Send(message);
        }
    }
}
