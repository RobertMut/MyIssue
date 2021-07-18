using MyIssue.Core.Entities;
using MyIssue.Server.Net;
using System.Threading;

namespace MyIssue.Server.Commands
{
    class WhoAmI : Cmd
    {
        public override void Command(Client client, CancellationToken ct)
        {
            LogUser.TypedCommand("WhoAmI", "Executed", client);
            string whoAreYou = string.Format("ID: {0}\r\nStatus: {1}\r\nLast Command: {2}\r\nAddress: {3}\r\n",
                client.Id, client.Status, client.CommandHistory[client.CommandHistory.Count - 1], client.ConnectedSock.RemoteEndPoint);
            NetWrite.Write(client.ConnectedSock, whoAreYou, ct);
        }
    }
}
