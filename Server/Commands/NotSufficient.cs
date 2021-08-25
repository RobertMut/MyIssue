using MyIssue.Server.Net;
using System.Threading;

namespace MyIssue.Server.Commands
{
    class NotSufficient : Command
    {
        public static string Name = "NotSufficient";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {
            LogUser.TypedCommand(client.CommandHistory[client.CommandHistory.Count - 1], "Tried to execute", client);
            NetWrite.Write(client.ConnectedSock, "INSUFFICIENT PERMISSIONS!\r\n", ct);
        }
    }
}
