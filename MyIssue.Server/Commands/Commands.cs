using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using MyIssue.Server.Net;

namespace MyIssue.Server
{
    public class Commands : IDBCommands, IUserCommands, ILogin
    {
        private INetwork _net;

        public void Login(string input, Client client, CancellationToken ct)
        {
            _net = new Network();
            if (input.Equals("admin"))
            {
                _net.Write(client.ConnectedSock, "Pass:\r\n", ct);
                string pass = _net.Receive(client.ConnectedSock, ct).Result;
                if (pass.Equals("1234"))
                {
                    Console.WriteLine("LOGGED!");
                    client.Status = 2;
                    Console.WriteLine(client.Status);
                }
                else
                {
                    Console.WriteLine("Err");
                }
            }
        }
        public void History(Client client, CancellationToken ct)
        {
            _net = new Network();
            string commandHistory = string.Join("\r\n", client.CommandHistory.ToArray()) + "\r\n";
            if (commandHistory.Length > 1024) commandHistory = commandHistory.Substring(0, 1019) + "\r\n";
            _net.Write(client.ConnectedSock, commandHistory, ct);
        }
        public void Disconnect(Client client, CancellationToken ct)
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                client.Terminated = true;
                Console.WriteLine("{0} - {1} - Disconnected by command", ClientCounter.Clients, client.ConnectedSock.LocalEndPoint);
                ClientCounter.Clients--;
                client.ConnectedSock.Close();
                client.ConnectedSock.Dispose();
                cts.Cancel();
            }
            
            
        }
        public void WhoAmI(Client client, CancellationToken ct)
        {
            _net = new Network();
            string whoAreYou = string.Format("ID: {0}\r\nStatus: {1}\r\nLast Command: {2}\r\nAddress: {3}",
                client.Id, client.Status, client.CommandHistory[client.CommandHistory.Count - 1], client.ConnectedSock.RemoteEndPoint);
            _net.Write(client.ConnectedSock, whoAreYou, ct);
        }
        public void CreateTask(Client client, CancellationToken ct)
        {
            _net = new Network();
            Database.DBConnector connector = new Database.DBConnector();
            Database.Queries dbquery = new Database.Queries();
            if (!client.Status.Equals(0))
            {
                _net.Write(client.ConnectedSock, "OK\r\n", ct);
                client.CommandHistory.Add(_net.Receive(client.ConnectedSock, ct).Result);
                string[] task = client.CommandHistory[client.CommandHistory.Count - 1].Split(new string[] { "\r\n<NEXT>\r\n" }, StringSplitOptions.None);
                var sqlparams = connector.SqlBuilder(Database.DBParameters.Parameters);
                var query = dbquery.InsertNewTask(task, "dbo.TASKS");
                connector.MakeQuery(sqlparams, query);

            }
            else
            {
                _net.Write(client.ConnectedSock, "Please log in first!\r\n", ct);
            }

        }

    }

}
