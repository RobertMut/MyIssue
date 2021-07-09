using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using MyIssue.Server.Tools;

namespace MyIssue.App
{
    class ConsoleClient
    {
        private Socket client;
        private readonly IPEndPoint endPoint;
        private readonly IStringTools _tools;
        private readonly string login;
        private readonly string pass;
        public ConsoleClient(string address, int port, string username, string password)
        {
            _tools = new StringTools();
            endPoint = new IPEndPoint(IPAddress.Parse(address), port);
            login = username;
            pass = password;
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Client(string command)
        {
                client.ReceiveTimeout = 5000;

                try
                {
                    client.Connect(endPoint);
                    SendData(command);
                    client.Disconnect(true);
                    client.Dispose();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());

                }
        }

        private void SendData(string commandToSend)
        {
            try
            {
                using (var ns = new NetworkStream(client))
                {
                    Console.WriteLine(ReadIncoming(ns));
                    var c = _tools.ByteMessage(ConsoleCommands.login);
                    ns.Write(c, 0, c.Length);
                    Console.WriteLine(ReadIncoming(ns));
                    ns.Flush();
                    c = _tools.ByteMessage(string.Format(ConsoleCommands.loginParameters, login, pass));
                    ns.Write(c, 0, c.Length);
                    ns.Flush();
                    Console.WriteLine(ReadIncoming(ns));
                    c = _tools.ByteMessage(ConsoleCommands.newTask);
                    ns.Write(c, 0, c.Length);
                    ns.Flush();
                    Console.WriteLine(ReadIncoming(ns));
                    c = _tools.ByteMessage(commandToSend);
                    ns.Write(c, 0, c.Length);
                    ns.Flush();
                    Console.WriteLine(commandToSend);
                    Console.WriteLine(ReadIncoming(ns));
                    c = _tools.ByteMessage(ConsoleCommands.logout);
                    ns.Write(c, 0, c.Length);
                    Console.WriteLine(ReadIncoming(ns));
                    Debug.WriteLine("Data sent!");
                    ns.Flush();
                    ns.Close();
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        private string ReadIncoming(NetworkStream ns)
        {
            byte[] read = new byte[2048];
            int s = ns.Read(read, 0, read.Length);
            return _tools.StringMessage(read,s);
        }
    }
}
