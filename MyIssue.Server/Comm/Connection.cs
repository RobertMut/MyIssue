using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    class Connection : Net
    {
        public override async Task Listen(string ipAddres, int port, int bufferSize = 1024)
        {
            //CancellationTokenSource tokenSource;
            Parameters.ConnBuffer = new byte[Parameters.BufferSize];
            base.EndPoint = SetEndPoint(ipAddres, port);
            IdentifyClient client = new IdentifyClient();
            try
            {
                ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ListenSocket.Bind(base.EndPoint);
                ListenSocket.Listen(100);
                CancellationTokenSource tokenSource;
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Accepting...");
                        Socket sock = ListenSocket.AcceptAsync().Result;
                        Console.WriteLine(sock.Connected.ToString());
                        await client.ConnectedTask(sock, 
                            (tokenSource = new CancellationTokenSource()).Token);

                    } catch (Exception e)
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
        public override string Receive(ref Socket sock, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            using (NetworkStream netStream = new NetworkStream(sock))
            {
                byte[] receiveBuffer = new byte[Parameters.BufferSize];
                netStream.ReadTimeout = Parameters.Timeout;
                try
                {
                    Tools t = new StringProcessing();
                    bool terminator = false;
                    string input = string.Empty;
                    int x = 0;
                    while (!terminator || !sock.Connected)
                    {
                        try
                        {
                            x = netStream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length).Result;
                            Console.WriteLine("received: {0}", x);
                            input += t.StringMessage(receiveBuffer, x);
                            int f = input.IndexOf("\r\n<EOF>\r\n");
                            Console.WriteLine("{0} - {1}", x, input);
                            if (x > 0 && !f.Equals(-1)) terminator = true;
                            //ct.ThrowIfCancellationRequested();
                        } catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                    }
                    Console.WriteLine(input);
                    input = input.Remove(input.Length - 9, 9);
                    Console.WriteLine(input);
                    return input;
                }
                catch (Exception e)
                {
                    Write(ref sock, e.Message, ct);
                    netStream.Close();
                    sock.Close();
                    IdentifyClient.Clients--;
                    return String.Empty;


                }
            }
        }
        public override void Write(ref Socket sock, string dataToSend, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            using (NetworkStream netStream = new NetworkStream(sock))
            {
                byte[] writeBuffer = new byte[Parameters.BufferSize];
                try
                {
                    Tools t = new StringProcessing();
                    writeBuffer = t.ByteMessage(dataToSend);
                    netStream.WriteAsync(writeBuffer, 0, writeBuffer.Length);
                }
                catch (Exception e)
                {
                    e.ToString();
                    netStream.Close();
                    sock.Close();
                    IdentifyClient.Clients--;
                }


            }

        }
        private IPEndPoint SetEndPoint(string ipAddr, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddr);
            return new IPEndPoint(ip, port);
        }
    }
}
