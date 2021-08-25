using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Core.Interfaces;
using MyIssue.Server.Model.Builders;
using MyIssue.Server.Net;

namespace MyIssue.Server.Client
{
    public class ProcessClient : IProcessClient
    {
        public async Task ConnectedTask(Socket sock, CancellationToken ct)
        {
            var client = ClientBuilder
                .Create()
                    .SetSocket(sock)
                    .SetId(ClientCounter.Clients)
                    .SetCommandHistory(new List<string>())
                    .SetStatus(0)
                    .SetTerminated(false)
                .Build();
            LogUser.TypedCommand("connected", "New client", client);
            Client(client, ct);
        }
        private void Client(Model.Client client, CancellationToken ct)
        {
            CommandParser parser = new CommandParser();
            using (NetworkStream netS = new NetworkStream(client.ConnectedSock))
            {
                string response = string.Empty;

                NetWrite.Write(client.ConnectedSock, "LOGIN\r\n", ct);
                while (!client.Terminated)
                {
                    ct.ThrowIfCancellationRequested();
                    parser.Parser(NetRead.Receive(client.ConnectedSock, ct).Result, client, ct);
                }
            }
        }
    }
}
