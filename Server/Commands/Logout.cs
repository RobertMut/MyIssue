using MyIssue.Core.Entities;
using System.Threading;

namespace MyIssue.Server.Commands
{
    class Logout : Command
    {
        public static string Name = "Logout";
        public override void Invoke(Client client, CancellationToken ct)
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                LogUser.TypedCommand("Disconnect", "Executed", client);
                client.Terminated = true;
                client.ConnectedSock.Close();
                client.ConnectedSock.Dispose();
                cts.Cancel();
            }


        }
    }
}
