using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    class AuthenticationCommands : IdentifyClient
    {
        
        public void Client(int client, ref Socket sock, CancellationToken ct)
        {
            Net n = new Connection();

            ct.ThrowIfCancellationRequested();
            using (NetworkStream netS = new NetworkStream(sock))
            {
                string response = string.Empty;
                Console.WriteLine("{0} - {1} - Waiting for HELLO", n.EndPoint, Clients);
                try
                {
                    
                    n.Write(ref sock, "HELLO\r\n", ct);
                    Console.WriteLine("1");
                    response = n.Receive(ref sock, ct);
                    if (!response.StartsWith("HELLO")) throw new Exception(n.EndPoint + " - Expected HELLO. Goodbye.");
                    Console.WriteLine("2");
                    n.Write(ref sock, client + ": AUTHENTICATION\r\n", ct);
                    response = n.Receive(ref sock, ct);
                    Command(response, ref sock, ct);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.ToString());
                    netS.Close();
                    sock.Close();
                    Clients--;
                }
            }
        }
        public void Command(string data, ref Socket sock, CancellationToken ct)
        {
            using (NetworkStream netStream = new NetworkStream(sock))
            {
                Net n = new Connection();

                string resp = string.Empty;
                try
                {
                    Tools t = new Tools();
                    
                    switch (new Regex(@"USER\s\S+").IsMatch(data))
                    {
                        case true:
                            Console.WriteLine("MATCH!");
                            n.Write(ref sock, "PASS\r\n", ct);
                            resp = n.Receive(ref sock, ct);

                            Console.WriteLine(resp);
                            if (t.UserPass(t.ExtractLogin(data), resp).Equals(true))
                            {
                                Console.WriteLine("ZALOGOWANO");
                            }
                            else
                            {
                                Console.WriteLine("Niezalogowano :(");
                            }
                            netStream.Flush();
                            netStream.Close();
                            Console.WriteLine("Closed connection");
                            break;
                        case false:
                            Console.WriteLine("NOT MATCH :(!");
                            netStream.Flush();
                            netStream.Close();
                            Console.WriteLine("Closed connection");
                            break;

                    }
                }
                catch (Exception e)
                {
                    netStream.Close();
                    sock.Close();
                    Clients--;
                    Console.WriteLine(e.ToString());
                }
            }



        }
    }
}
