using MyIssue.Server.Net;
using System.Threading;

namespace MyIssue.Server.Commands
{
    class WhoAmI : Command
    {
        public static string Name = "WhoAmI";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {
            LogUser.TypedCommand("WhoAmI", "Executed", client);
            string whoAreYou =
                $"ID: {client.Login}\r\nStatus: {client.Status}\r\nLast Command: {client.CommandHistory[client.CommandHistory.Count - 1]}\r\nAddress: {client.ConnectedSock.RemoteEndPoint}\r\n";
            NetWrite.Write(client.ConnectedSock, whoAreYou, ct);
        }
    }
}
