using MyIssue.Core.Entities;
using MyIssue.Server.Net;
using System.Threading;

namespace MyIssue.Server.Commands
{
    class NotSufficient : Cmd
    {
        public override void Command(Client client, CancellationToken ct)
        {
            LogUser.TypedCommand(client.CommandHistory[client.CommandHistory.Count - 1], "Tried to execute", client);
            NetWrite.Write(client.ConnectedSock, "INSUFFICIENT PERMISSIONS!\r\n", ct);
        }
    }
}
