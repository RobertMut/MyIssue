using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Imap
{
    class ImapParametersBuilder : IImapParametersBuilder
    {
        protected ImapParametersTemplate template;
        private ImapParametersBuilder()
        {
            template = new ImapParametersTemplate();
        }
        public IImapParametersBuilder SetAddress(string address)
        {
            template.Address = address;
            return this;
        }
        public IImapParametersBuilder SetPort(int port)
        {
            template.Port = port;
            return this;
        }
        public IImapParametersBuilder SetSocketOptions(SecureSocketOptions socketOptions)
        {
            template.SocketOptions = socketOptions;
            return this;
        }
        public IImapParametersBuilder SetLogin(string login)
        {
            template.Login = login;
            return this;
        }
        public IImapParametersBuilder SetPassword(string password)
        {
            template.Password = password;
            return this;
        }
        public ImapParametersTemplate Build() => template;
        public static ImapParametersBuilder Create() => new ImapParametersBuilder();
    }
}
