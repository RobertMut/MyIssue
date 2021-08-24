using MyIssue.Server.Net;
using System.Threading;

namespace MyIssue.Server.Commands
{
    public class NotFound : Command
    {
        public static string Name = "NotFound";
        public override void Invoke(Entities.Client client, CancellationToken ct)
        {
            NetWrite.Write(client.ConnectedSock, "Command not found!\r\n", ct);
        }
    }
}
