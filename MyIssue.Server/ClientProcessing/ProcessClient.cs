using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MyIssue.Server.Net;

namespace MyIssue.Server
{
    public class ProcessClient : IProcessClient
    {

        private IClientBuilder _iCli;
        private IdentifyClient iC;
        private INetwork _net;

        public async Task ConnectedTask(Socket sock, CancellationToken ct)
        {
            _iCli = new ClientBuilder();
            iC = new IdentifyClient();
            var client = iC.Identify(_iCli, sock, ClientCounter.Clients, new List<string>(), 0, false);
            //Console.WriteLine(client.ConnectedSock.RemoteEndPoint);
            Client(client, ct);
        }
        private void Client(Client client, CancellationToken ct)
        {
            _net = new Network();
            CommandParser parser = new CommandParser();
            using (NetworkStream netS = new NetworkStream(client.ConnectedSock))
            {
                string response = string.Empty;
                Console.WriteLine("{0} - {1} - Waiting for Login", Parameters.EndPoint, client.Id);

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
