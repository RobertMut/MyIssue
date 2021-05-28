﻿using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

using MyIssue.Server.Net;
using MyIssue.Server.Commands;

namespace MyIssue.Server
{
    public class ProcessClient : IProcessClient
    {
        private INetwork _net;
        public ProcessClient()
        {
            _net = new Network();
        }

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
        private void Client(Client client, CancellationToken ct)
        {
            CommandParser parser = new CommandParser();
            using (NetworkStream netS = new NetworkStream(client.ConnectedSock))
            {
                string response = string.Empty;

                _net.Write(client.ConnectedSock, "LOGIN\r\n", ct);
                while (!client.Terminated)
                {
                    ct.ThrowIfCancellationRequested();
                    parser.Parser(_net.Receive(client.ConnectedSock, ct).Result, client, ct);
                }
            }
        }
    }
}
