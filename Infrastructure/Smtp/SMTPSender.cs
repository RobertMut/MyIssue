using System;
using System.Net;
using System.Net.Mail;
using MyIssue.Core.Entities;
using MyIssue.Core.Interfaces;

namespace MyIssue.Infrastructure.Smtp
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
