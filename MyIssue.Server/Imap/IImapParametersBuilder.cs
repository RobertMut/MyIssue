using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Imap
{
    public interface IImapParametersBuilder
    {
        IImapParametersBuilder SetAddress(string address);
        IImapParametersBuilder SetPort(int port);
        IImapParametersBuilder SetSocketOptions(SecureSocketOptions socketOptions);
        IImapParametersBuilder SetLogin(string login);
        IImapParametersBuilder SetPassword(string password);
        ImapParametersTemplate Build();
    }
}
