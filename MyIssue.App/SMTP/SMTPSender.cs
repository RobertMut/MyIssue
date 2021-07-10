using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using MyIssue.DesktopApp.IO;
using MyIssue.Core.Interfaces;
using MyIssue.Core.IO;

namespace MyIssue.DesktopApp.SMTP
{
    public class SMTPSender : ISMTPSender
    {
        private readonly SmtpClient client;
        private IDecryptedValue _value;
        private readonly string appPass;
        private IReadConfig _read;

        public SMTPSender(string applicationPassword)
        {
            _value = new DecryptedValue(Paths.confFile);
            appPass = applicationPassword;
            client = new SmtpClient(_value.GetValue("serverAddress",appPass), 
                Int32.Parse(_value.GetValue("port",appPass))
                );
            _read = new OpenConfiguration();
            
        }
        public void SendMessage(MailMessage message)
        {
            _value = new DecryptedValue(Paths.confFile);
            client.Credentials = new NetworkCredential(_value.GetValue("login", appPass), _value.GetValue("pass", appPass));
            client.EnableSsl = Convert.ToBoolean(ConfigValue.GetValue("sslTsl",_read.OpenConfig(Paths.confFile)));
            client.Send(message);
        }
    }
}
