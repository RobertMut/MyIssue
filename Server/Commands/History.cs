using MyIssue.Core.Entities;
using MyIssue.Server.Net;
using System.Threading;
using Client = MyIssue.Server.Entities.Client;

namespace MyIssue.Server.Commands
{
    class History : Command
    {
        public static string Name = "History";
        public override void Invoke(Entities.Client client, CancellationToken ct)
        {
            LogUser.TypedCommand("History", "Executed", client);
            string commandHistory = string.Join("\r\n", client.CommandHistory.ToArray()) + "\r\n";
            if (commandHistory.Length > 1024) commandHistory = commandHistory.Substring(0, 1019) + "\r\n";
            NetWrite.Write(client.ConnectedSock, commandHistory, ct);
        }


    }
}
