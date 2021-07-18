using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using MyIssue.Core.String;

namespace MyIssue.DesktopApp.Model
{
    class ConsoleClient
    {
        private IExceptionMessageBox _exceptionMessage;
        private Socket client;
        private readonly IPEndPoint endPoint;
        private readonly string login;
        private readonly string pass;
        public ConsoleClient(string address, int port, string username, string password)
        {
            endPoint = new IPEndPoint(IPAddress.Parse(address), port);
            login = username;
            pass = password;
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _exceptionMessage = new ExceptionMessageBox();
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
                _exceptionMessage.ShowException(e);

            }
        }

        private void SendData(string commandToSend)
        {
            try
            {
                using (var ns = new NetworkStream(client))
                {
                    Console.WriteLine(ReadIncoming(ns));
                    var c = StringStatic.ByteMessage(ConsoleCommands.login);
                    ns.Write(c, 0, c.Length);
                    Console.WriteLine(ReadIncoming(ns));
                    ns.Flush();
                    c = StringStatic.ByteMessage(string.Format(ConsoleCommands.loginParameters, login, pass));
                    ns.Write(c, 0, c.Length);
                    ns.Flush();
                    Console.WriteLine(ReadIncoming(ns));
                    c = StringStatic.ByteMessage(ConsoleCommands.newTask);
                    ns.Write(c, 0, c.Length);
                    ns.Flush();
                    Console.WriteLine(ReadIncoming(ns));
                    c = StringStatic.ByteMessage(commandToSend);
                    ns.Write(c, 0, c.Length);
                    ns.Flush();
                    Console.WriteLine(commandToSend);
                    Console.WriteLine(ReadIncoming(ns));
                    c = StringStatic.ByteMessage(ConsoleCommands.logout);
                    ns.Write(c, 0, c.Length);
                    Console.WriteLine(ReadIncoming(ns));
                    Debug.WriteLine("Data sent!");
                    ns.Flush();
                    ns.Close();
                }

            }
            catch (Exception e)
            {
                _exceptionMessage.ShowException(e);
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
