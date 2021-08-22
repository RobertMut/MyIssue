using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Core.Interfaces;
using MyIssue.Core.Entities;
using MyIssue.Infrastructure.Files;

namespace MyIssue.Server.Net
{
    public class NetListener : INetwork
    {
        private IProcessClient _processClient;
        public NetListener(string ipAddres, int port)
        {
            _processClient = new ProcessClient();
            Parameters.ConnBuffer = new byte[Parameters.BufferSize];
            Parameters.EndPoint = SetEndPoint(ipAddres, port);
            Parameters.ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Listen()
        {
            try
            {
                Parameters.ListenSocket.Bind(Parameters.EndPoint);
                Parameters.ListenSocket.Listen(100);
                CancellationTokenSource ct;
                while (true)
                {
                    Console.WriteLine("Server - {0} - Accepting incoming connections..", DateTime.Now);
                    Socket sock = Parameters.ListenSocket.AcceptAsync().Result;
                    Task.Run(async () =>
                    {
                        ct = new CancellationTokenSource();
                        _processClient.ConnectedTask(sock, ct.Token);
                    }
                    );
                }
            }
            catch (ArgumentNullException ane)
            {
                SerilogLogger.ServerLogException(ane);
                
            } catch (SocketException se)
            {
                SerilogLogger.ServerLogException(se);
            }
        }
        private IPEndPoint SetEndPoint(string ipAddr, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddr);
            return new IPEndPoint(ip, port);
        }
    }
}
