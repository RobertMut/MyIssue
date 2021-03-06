using System.Threading;

namespace MyIssue.Server.Commands
{
    class Logout : Command
    {
        public static string Name = "Logout";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                LogUser.TypedCommand("Disconnect", "Executed", client);
                client.Terminated = true;
                client.ConnectedSock.Close();
                client.ConnectedSock.Dispose();
                httpclient.Dispose();
                cts.Cancel();
            }


        }
    }
}
