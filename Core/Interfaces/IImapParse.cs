using MimeKit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Core.Interfaces
{
    public interface IImapParse
    {
        Task ImapListenNewMessagesAsync(CancellationToken ct);
        void WriteToDatabase(MimeMessage m);
        void Inbox_CountChangedAsync(object sender, EventArgs e);
    }
}
