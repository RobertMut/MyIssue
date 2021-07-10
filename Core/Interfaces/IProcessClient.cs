using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Core.Interfaces
{
    public interface IProcessClient
    {
        Task ConnectedTask(Socket sock, CancellationToken ct);
    }
}
