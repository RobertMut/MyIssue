using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    class IdentifyClient
    {
        public static int Clients { get; set; }
        public bool status { get; set; } = false;
        public Task ConnectedTask(Socket sock, CancellationToken ct)
        {
            Identify(sock, ct);
            return Task.CompletedTask;
        }
        private void Identify(Socket sock, CancellationToken ct)
        {
            try
            {
                Commands ac = new Commands();
                Console.WriteLine("Connected");
                var cli = new ClientIdentifier()
                {
                    ConnectedSock = sock,
                    Id = Clients++,
                    CommandHistory = new List<string>(),
                    Status = 0,
                    terminated = false
                };
                ac.Client(cli, ct);
                ct.ThrowIfCancellationRequested();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                sock.Close();

            }

        }
    }
}
