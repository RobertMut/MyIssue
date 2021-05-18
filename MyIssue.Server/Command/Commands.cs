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
        Comm.ICommunicate comm = new Comm.Communicate();

        public void Login(string input, ClientIdentifier client, CancellationToken ct)
        {
            if (input.Equals("admin"))
            {
                comm.Write(client.ConnectedSock, "Pass:\r\n", ct);
                string pass =  comm.Receive(client.ConnectedSock, ct);
                if (pass.Equals("1234"))
                {
                    Console.WriteLine("LOGGED!");
                    client.Status = 2;
                    client.loggedIn = true;
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
            
            string commandHistory = string.Join("\r\n", client.CommandHistory.ToArray())+"\r\n";
            if (commandHistory.Length > 1024) commandHistory = commandHistory.Substring(0, 1019)+"\r\n";
            comm.Write(client.ConnectedSock, commandHistory, ct);
        }
        public void CreateTask(ClientIdentifier client, CancellationToken ct)
        {
            Database.DBConnector connector = new Database.DBConnector();
            Database.Queries dbquery = new Database.Queries();
            if (client.loggedIn)
            {
                comm.Write(client.ConnectedSock, "OK\r\n", ct);
                client.CommandHistory.Add(comm.Receive(client.ConnectedSock, ct));
                string[] task = client.CommandHistory[client.CommandHistory.Count - 1].Split(new string[] { "\r\n<NEXT>\r\n" }, StringSplitOptions.None);
                var sqlparams = connector.SqlBuilder(Program.dbParameters);
                var query = dbquery.InsertNewTask(task, "dbo.TASKS");
                connector.MakeQuery(sqlparams, query);

            } else
            {
                comm.Write(client.ConnectedSock, "Please log in first!\r\n", ct);
            }

        }
        public void Disconnect(ClientIdentifier client, CancellationToken ct)
        {
            client.terminated = true;
            Console.WriteLine("{0} - {1} - Disconnected by command", ClientCounter.Clients, client.ConnectedSock.LocalEndPoint);
            ClientCounter.Clients--;
            client.ConnectedSock.Close();
            client.ConnectedSock.Dispose();
        }
        public void Client(ClientIdentifier client, CancellationToken ct) //move
        {
            
            CommandParser parser = new CommandParser();
            ct.ThrowIfCancellationRequested();
            using (NetworkStream netS = new NetworkStream(client.ConnectedSock))
            {
                string response = string.Empty;
                Console.WriteLine("{0} - {1} - Waiting for Login", Comm.Parameters.EndPoint, client.Id);
                try
                {

                    comm.Write(client.ConnectedSock, "HELLO\r\n", ct);
                    Console.WriteLine("1");
                    while (!client.terminated)
                    {
                        parser.Parser(comm.Receive(client.ConnectedSock, ct), client, ct);
                    }
                    

                    if (!client.CommandHistory[client.CommandHistory.Count - 1].StartsWith("Login"))
                        throw new Exception(Comm.Parameters.EndPoint + " - Ex."); //move
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.ToString());
                    netS.Close();
                    client.ConnectedSock.Close();
                    ClientCounter.Clients--;
                }
            }
        }

    }
}
