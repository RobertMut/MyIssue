using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server.Imap
{
    public interface IImapParse
    {
        Task ImapListenNewMessagesAsync(CancellationToken ct);
        void WriteToDatabase(MimeMessage m);
        void Inbox_CountChangedAsync(object sender, EventArgs e);
    }
}
