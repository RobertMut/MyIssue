using MailKit.Net.Imap;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Core.Interfaces
{
    public interface IImapConnect
    {
        Task RunImap(CancellationToken ct);
        Task ConnectToImap(ImapClient c, CancellationToken ct);
        Task ReconnectAsync(ImapClient c);
    }
}
