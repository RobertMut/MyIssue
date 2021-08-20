using MyIssue.Core.Entities;
using MyIssue.Server.Net;
using System;
using System.Threading;

namespace MyIssue.Server.Commands
{
    public class NotFound : Command
    {
        public static string Name = "NotFound";
        public override void Invoke(Client client, CancellationToken ct)
        {
            NetWrite.Write(client.ConnectedSock, "Command not found!\r\n", ct);
        }
    }
}
