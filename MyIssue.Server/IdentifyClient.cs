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
        public Task ConnectedTask(Socket sock, CancellationToken ct)
        {
            Connected(sock, ct);
            return Task.CompletedTask;
        }
        private void Connected(Socket sock, CancellationToken ct)
        {
            try
            {
                AuthenticationCommands ac = new AuthenticationCommands();
                Console.WriteLine("Connected");
                Clients++;
                ac.Client(Clients, ref sock, ct);
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
