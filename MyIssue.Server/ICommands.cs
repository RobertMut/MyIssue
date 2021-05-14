using System;
using System.Threading;

namespace MyIssue.Server
{
    public interface ICommands
    {
        void Login(string input, ClientIdentifier client, CancellationToken ct);
        void Client(ClientIdentifier client, CancellationToken ct);
        void History(ClientIdentifier client, CancellationToken ct);
        void Disconnect(ClientIdentifier client, CancellationToken ct);
    }

}

