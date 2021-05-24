using System;
using System.Collections.Generic;
using System.Data;
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

        public void Login(Client client, CancellationToken ct)
        {
            _net = new Network();
            _net.Write(client.ConnectedSock, "LOGGING IN\r\n", ct);
            client.CommandHistory.Add(_net.Receive(client.ConnectedSock, ct).Result);
            string[] task = client.CommandHistory[client.CommandHistory.Count - 1].Split(new string[] { "\r\n<NEXT>\r\n" }, StringSplitOptions.None);
            Database.DBConnector connector = new Database.DBConnector();
            Database.Queries dbquery = new Database.Queries();
            var sqlparams = connector.SqlBuilder(Database.DBParameters.Parameters);
            var query = dbquery.Login(task, Database.DBParameters.Parameters.UsersTable);
            DataTable s = connector.MakeReadQuery(sqlparams, query);
            Console.WriteLine(s.Rows[0]);
            if (s.Rows[0][0].Equals(task[0]) && s.Rows[0][1].Equals(task[1]))
            {
                Console.WriteLine("LOGGED!");
                client.Status = Convert.ToInt32(s.Rows[0][2]);
                _net.Write(client.ConnectedSock, "LOGGED!\r\n", ct);
                
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
            string whoAreYou = string.Format("ID: {0}\r\nStatus: {1}\r\nLast Command: {2}\r\nAddress: {3}\r\n",
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
                _net.Write(client.ConnectedSock, "CREATING TASK\r\n", ct);
                client.CommandHistory.Add(_net.Receive(client.ConnectedSock, ct).Result);
                string[] task = client.CommandHistory[client.CommandHistory.Count - 1].Split(new string[] { "\r\n<NEXT>\r\n" }, StringSplitOptions.None);
                var sqlparams = connector.SqlBuilder(Database.DBParameters.Parameters);
                var query = dbquery.InsertNewTask(task, "dbo.TASKS");
                connector.MakeWriteQuery(sqlparams, query);

            }
            else
            {
                _net.Write(client.ConnectedSock, "NOT LOGGED IN\r\n", ct);
            }

        }
        public void AddEmployee(Client client, CancellationToken ct)
        {

            if (client.Status.Equals(2))
            {
                _net = new Network();
                Database.DBConnector connector = new Database.DBConnector();
                Database.Queries dbquery = new Database.Queries();

                _net.Write(client.ConnectedSock, "ADD EMPLOYEE\r\n", ct);
                client.CommandHistory.Add(_net.Receive(client.ConnectedSock, ct).Result);
                string[] value = client.CommandHistory[client.CommandHistory.Count - 1].Split(new string[] { "\r\n<NEXT>\r\n" }, StringSplitOptions.None);
                var employeequery = dbquery.AddEmployee(value.Skip(3).ToArray(), Database.DBParameters.Parameters.EmployeesTable);
                var userquery = dbquery.AddUser(value.Take(3).ToArray(), Database.DBParameters.Parameters.UsersTable);
                var sqlparams = connector.SqlBuilder(Database.DBParameters.Parameters);
                connector.MakeWriteQuery(sqlparams, userquery);
                connector.MakeWriteQuery(sqlparams, employeequery);
            }
            else
            {
                _net.Write(client.ConnectedSock, "INSUFFICIENT PERMISSIONS!\r\n", ct);
            }
        }
        public void AddUser(Client client, CancellationToken ct)
        {

            if (client.Status.Equals(2))
            {
                _net = new Network();
                Database.DBConnector connector = new Database.DBConnector();
                Database.Queries dbquery = new Database.Queries();

                _net.Write(client.ConnectedSock, "ADD USER\r\n", ct);
                client.CommandHistory.Add(_net.Receive(client.ConnectedSock, ct).Result);
                string[] value = client.CommandHistory[client.CommandHistory.Count - 1].Split(new string[] { "\r\n<NEXT>\r\n" }, StringSplitOptions.None);
                var query = dbquery.AddUser(value, Database.DBParameters.Parameters.UsersTable);
                var sqlparams = connector.SqlBuilder(Database.DBParameters.Parameters);
                connector.MakeWriteQuery(sqlparams, query);
            }
            else
            {
                _net.Write(client.ConnectedSock, "INSUFFICIENT PERMISSIONS!\r\n", ct);
            }
        }

    }

}
