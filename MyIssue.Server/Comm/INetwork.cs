using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server.Comm
{
    public interface ICommunicate
    {
        string Receive(Socket sock, CancellationToken ct);
        void Write(Socket sock, string dataToSend, CancellationToken ct);
    }
    public interface IListen
    {
        Task Listener(string ipAddres, int port);
    }
}
