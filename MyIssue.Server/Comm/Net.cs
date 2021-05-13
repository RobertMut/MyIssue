using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public abstract class Net
    {
        public IPEndPoint EndPoint { get; protected set; }
        protected static Socket ListenSocket { get; set; }

        public abstract Task Listen(string ipAddres, int port, int bufferSize = 1024);
        public abstract string Receive(Socket sock, CancellationToken ct);
        public abstract void Write(Socket sock, string dataToSend, CancellationToken ct);

    }
}
