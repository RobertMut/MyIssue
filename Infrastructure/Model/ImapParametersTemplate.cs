using MailKit.Security;

namespace MyIssue.Infrastructure.Model
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
