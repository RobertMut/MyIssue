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
        private Socket client;
        private readonly IPEndPoint endPoint;

        public ServerConnector(string serverAddress, int port)
        {
            endPoint = new IPEndPoint(IPAddress.Parse(serverAddress), port);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public string SendData(IEnumerable<byte[]> commandsToSend)
        {
            string response = string.Empty;
            try
            {
                if (commandsToSend.Count() is 0) throw new IndexOutOfRangeException();
                client.Connect(endPoint);
                using (NetworkStream ns = new NetworkStream(client))
                {
                    Console.WriteLine(ReadIncoming(ns));
                    foreach (var bytes in commandsToSend)
                    {
                        Console.WriteLine(StringStatic.StringMessage(bytes, bytes.Length));
                        ns.Write(bytes, 0, bytes.Length);
                        string workstring = ReadIncoming(ns);
                        if (!string.IsNullOrEmpty(workstring)) response = workstring;
                    }
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
            byte[] read = new byte[2048];
            int s = ns.Read(read, 0, read.Length);
            return StringStatic.StringMessage(read, s);
        }
    }

}