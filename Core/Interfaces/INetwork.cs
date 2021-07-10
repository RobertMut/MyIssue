using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Core.Interfaces
{
    public interface INetwork
    {
        Task<string> Receive(Socket sock, CancellationToken ct);
        void Write(Socket sock, string dataToSend, CancellationToken ct);
        void Listener(string ipAddres, int port);
    }
}
