using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using MyIssue.Core.Entities;
using MyIssue.Core.Interfaces;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Files;

namespace MyIssue.DesktopApp.Misc.Sender
{
    class ConsoleClient : IConsoleClient
    {
        private Socket client;
        private readonly IPEndPoint endPoint;

        private readonly List<string> consoleCommands;
        public ConsoleClient(SettingTextBoxes setting, IEnumerable<string> commands)
        {
            endPoint = new IPEndPoint(IPAddress.Parse(setting.ServerAddress), Int32.Parse(setting.Port));

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            consoleCommands = commands.ToList();
        }
        public void Client()
        {

            try
            {
                client.Connect(endPoint);
                SendData();
                client.Disconnect(true);
                client.Dispose();
            }
            catch (Exception e)
            {
                SerilogLogger.ClientLogException(e);
            }
        }
        private void SendData()
        {
            try
            {
                using (var ns = new NetworkStream(client))
                {
                    Console.WriteLine(ReadIncoming(ns));
                    consoleCommands.ForEach(c =>
                    {
                        var b = StringStatic.ByteMessage(c);
                        ns.Write(b, 0, b.Length);
                        Console.WriteLine(ReadIncoming(ns));
                    });
                    ns.Flush();
                    ns.Close();
                }

            }
            catch (Exception e)
            {
                SerilogLogger.ClientLogException(e);
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
