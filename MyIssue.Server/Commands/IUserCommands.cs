﻿using System.Threading;

namespace MyIssue.Server.Commands
{
    public interface IUserCommands
    {
        void WhoAmI(Client client, CancellationToken ct);
        void History(Client client, CancellationToken ct);
        void Disconnect(Client client, CancellationToken ct);
    }
}
