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
    public class Commands : ICommands
    {


        public void Login(string input, ClientIdentifier client, CancellationToken ct)
        {
            Net net = new Connection();
            if (input.Equals("admin"))
            {
                net.Write(client.ConnectedSock, "Pass:\r\n", ct);
                client.CommandHistory.Add(net.Receive(client.ConnectedSock, ct));
                if (client.CommandHistory[client.CommandHistory.Count - 1].Equals("1234"))
                {
                    Console.WriteLine("LOGGED!");
                    client.Status = 3;
                    Console.WriteLine(client.Status);
                }
                else
                {
                    Console.WriteLine("Err");
                }
            }
        }
        public void History(ClientIdentifier client, CancellationToken ct)
        {
            Net net = new Connection();
            string commandHistory = string.Join("\r\n", client.CommandHistory.ToArray())+"\r\n";
            if (commandHistory.Length > 1024) commandHistory = commandHistory.Substring(0, 1019)+"\r\n";
            net.Write(client.ConnectedSock, commandHistory, ct);
        }
        public void Disconnect(ClientIdentifier client, CancellationToken ct)
        {
            client.terminated = true;
            Console.WriteLine("{0} - {1} - Disconnected by command", IdentifyClient.Clients, client.ConnectedSock.LocalEndPoint);
            IdentifyClient.Clients--;
            client.ConnectedSock.Close();
            client.ConnectedSock.Dispose();
        }
        public void Client(ClientIdentifier client, CancellationToken ct) //move
        {
            Net n = new Connection();
            CommandParser parser = new CommandParser();
            ct.ThrowIfCancellationRequested();
            using (NetworkStream netS = new NetworkStream(client.ConnectedSock))
            {
                string response = string.Empty;
                Console.WriteLine("{0} - {1} - Waiting for Login", n.EndPoint, client.Id);
                try
                {

                    n.Write(client.ConnectedSock, "HELLO\r\n", ct);
                    Console.WriteLine("1");
                    while (!client.terminated)
                    {
                        parser.Parser(n.Receive(client.ConnectedSock, ct), client, ct);
                    }
                    

                    if (!client.CommandHistory[client.CommandHistory.Count - 1].StartsWith("Login"))
                        throw new Exception(n.EndPoint + " - Expected Login. Goodbye."); //move
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.ToString());
                    netS.Close();
                    client.ConnectedSock.Close();
                    IdentifyClient.Clients--;
                }
            }
        }

    }
}
