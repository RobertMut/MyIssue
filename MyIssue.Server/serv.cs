using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public class Serv
    {
        public static IPAddress host = IPAddress.Parse("127.0.0.1");
        public static int port = 49153;
        protected IPEndPoint localEndPoint = new IPEndPoint(host, port);
        private static Socket listenSocket;
        public int timeout = 10000;
        public byte[] toSend;
        public byte[] buffer = new byte[1024];
        public string strinbBuffer = string.Empty;
        public CancellationTokenSource cts = new CancellationTokenSource();
        public int clients = 0;

        public Task Listener()
        {
            try
            {
                listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listenSocket.Bind(localEndPoint);
                listenSocket.Listen(100);
                Console.WriteLine(buffer[0].ToString());

                while (true)
                {
                    //resetMe.Reset();
                    Console.WriteLine("Accepting...");
                    Socket sock = listenSocket.Accept();
                    Task.Run( async () => await Connected(sock));
                    //await Connected(listenSocket);
                    //listenSocket.BeginAccept(new AsyncCallback(Connected), listenSocket);

                    //resetMe.WaitOne();

                }
                    

            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return Task.CompletedTask;
        }

        private async Task Connected(Socket sock)
        {
            try
            {

                Console.WriteLine("Connected");
                clients++;
                await Task.Run(async () => await Client(clients, sock));
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                sock.Close();
            }

        }

        private async Task Client (int client, Socket sock)
        {

            using (NetworkStream netS = new NetworkStream(sock))
            {
                string response = string.Empty;
                Console.WriteLine("{0} - {1} - Waiting for HELLO", localEndPoint, clients);
                try
                {

                    await Write(sock, "HELLO\r\n");
                    Console.WriteLine("1");
                    response = Receive(sock).Result;
                    if (!response.StartsWith("HELLO")) throw new Exception();
                    Console.WriteLine("2");
                    await Write(sock, client + ": AUTHENTICATION\r\n");
                    response = Receive(sock).Result;
                    Command(response, sock);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    netS.Flush();
                    netS.Close();
                    sock.Close();
                }
            }


        }

        private Task<string> Receive(Socket sock)
        {
            using (NetworkStream netStream = new NetworkStream(sock))
            {
                netStream.ReadTimeout = timeout;
                byte[] initialBuffer = new byte[1024];
                byte[] targetBuffer;
                bool terminator = false;
                //bool res = false;

                //while (data.Equals(0))
                //{
                int x = 0;
                while(!terminator || !sock.Connected)
                {
                    x = netStream.ReadAsync(initialBuffer, 0, initialBuffer.Length).Result;
                    if (x > 0)
                    {
                        terminator = true;
                    }
                }
                Console.WriteLine((char)initialBuffer[initialBuffer.Length - 1]);
                targetBuffer = new byte[x];
                Array.Copy(initialBuffer, targetBuffer, x);
                Console.WriteLine("{0}",StringMessage(targetBuffer));
                return Task.FromResult(StringMessage(targetBuffer));
            }
        }
        private Task Write(Socket sock, string dataToSend)
        {
            using (NetworkStream netStream = new NetworkStream(sock)) {
                buffer = ByteMessage(dataToSend);
                Task waitWrite = netStream.WriteAsync(buffer, 0, buffer.Length);
                waitWrite.Wait();
                
            }

            return Task.CompletedTask;
        }

        private void Command(string data, Socket sock)
        {
            using (NetworkStream netStream = new NetworkStream(sock))
            {
                string resp = string.Empty;
                try
                {

                    switch (new Regex(@"USER\s\S+").IsMatch(data))
                    {
                        case true:
                            Console.WriteLine("MATCH!");
                            Write(sock, "PASS\r\n");
                            Task<string> r3 = Receive(sock);
                            //r3.Start();
                            r3.Wait();
                            resp = r3.Result;
                            
                            Console.WriteLine(resp);
                            if (UserPass(ExtractLogin(data), resp).Equals(true))
                            {
                                Console.WriteLine("ZALOGOWANO");
                                //return Task.Factory.FromAsync<int>();
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
                            //
                            //PROBLEM ODRAZU PRZECHODZI TU
                            //
                            netStream.Flush();
                            netStream.Close();
                            Console.WriteLine("Closed connection");
                            break;

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            
            

        }

        private static byte[] ByteMessage(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }
        private static string StringMessage(byte[] input)
        {
            return Encoding.UTF8.GetString(input);
        }
        private static string ExtractLogin(string loginInput)
        {
            Console.WriteLine(loginInput);
            //Console.WriteLine();
            //int last = new Regex(@"\S").Match(loginInput).Index;
            //Console.WriteLine(last);
            return loginInput.Remove(0, loginInput.IndexOf(' ')+1);
        }
        private static string ClearString(string input, int mode)
        {
            switch (mode)
            {
                case 0:
                    return Regex.Replace(input, @"[ \t\n\r\u0000]", "");
                case 1:
                    return Regex.Replace(input, @"[\n\r\u0000]", "");
                case 2:
                    return Regex.Replace(input, @"[\u0000]", "");
                default:
                    return input;
            }
            
        } 
        private static bool UserPass(string login, string pass)
        {
            if (String.Equals(login, "admin") && String.Equals(pass, "1234")) {
                return true;
            } else
            {
                return false;
            }
            
        }

    }
}
