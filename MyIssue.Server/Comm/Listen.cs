using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server.Comm
{
    public class Listen : IListen
    {
        public async Task Listener(string ipAddres, int port)
        {
            //CancellationTokenSource tokenSource;
            Parameters.ConnBuffer = new byte[Parameters.BufferSize];
            Parameters.EndPoint = SetEndPoint(ipAddres, port);
            IdentifyClient client = new IdentifyClient();
            try
            {
                Parameters.ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Parameters.ListenSocket.Bind(Parameters.EndPoint);
                Parameters.ListenSocket.Listen(100);
                CancellationTokenSource tokenSource;
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Accepting...");
                        Socket sock = Parameters.ListenSocket.AcceptAsync().Result;
                        Console.WriteLine(sock.Connected.ToString());
                        await client.ConnectedTask(sock,
                            (tokenSource = new CancellationTokenSource()).Token);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("token!");
            }

        }
        private IPEndPoint SetEndPoint(string ipAddr, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddr);
            return new IPEndPoint(ip, port);
        }
    }
}