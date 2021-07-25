using MyIssue.Core.Entities;
using MyIssue.Server.Net;
using System;
using System.Threading;

namespace MyIssue.Server.Commands
{
    public class NotFound : Cmd
    {
        public override void Command(Client client, CancellationToken ct)
        {
            NetWrite.Write(client.ConnectedSock, "Command not found!\r\n", ct);
        }
    }
}
