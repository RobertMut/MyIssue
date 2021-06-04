using MailKit.Net.Imap;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server.Imap
{
    public interface IImapConnect
    {
        Task RunImap(CancellationToken ct);
        Task ConnectToImap(ImapClient c, CancellationToken ct);
        Task ReconnectAsync(ImapClient c);
    }
}
