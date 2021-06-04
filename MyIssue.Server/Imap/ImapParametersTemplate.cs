using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyIssue.Server.Imap
{
    public class ImapParametersTemplate
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public SecureSocketOptions SocketOptions {get; set;}
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
