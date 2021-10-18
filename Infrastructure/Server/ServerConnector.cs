using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using MyIssue.Core.String;

namespace MyIssue.Infrastructure.Server
{
    public class ServerConnector : IServerConnector
    {
        private readonly IPEndPoint endPoint;
        public ServerConnector(string serverAddress, int port)
        {
            endPoint = new IPEndPoint(IPAddress.Parse(serverAddress), port);

        }

        public string SendData(IEnumerable<byte[]> commandsToSend)
        {
            string response = string.Empty;
            try
            {
                if (commandsToSend.Count() is 0) throw new IndexOutOfRangeException();
                using (Socket cli = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    cli.Connect(endPoint);
                    using (NetworkStream ns = new NetworkStream(cli))
                    {
                        ReadIncoming(ns);
                        foreach (var bytes in commandsToSend)
                        {
                            //Console.WriteLine(StringStatic.StringMessage(bytes, bytes.Length));
                            ns.Write(bytes, 0, bytes.Length);
                            string workstring = ReadIncoming(ns);
                            //Console.WriteLine(workstring);
                            if (!string.IsNullOrEmpty(workstring)) response = workstring;
                        }
                        ns.Close();
                    }
                    cli.Shutdown(SocketShutdown.Both);
                    cli.Disconnect(true);
                }
                return response;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("No commands to send!");
                return response;
            }
            catch (IOException iex)
            {
                Console.WriteLine(iex);
                return response;
            }
        }
        private string ReadIncoming(NetworkStream ns)
        {
            byte[] read = new byte[16384];
            int s = ns.Read(read, 0, read.Length);
            return StringStatic.StringMessage(read, s);
        }
    }

}