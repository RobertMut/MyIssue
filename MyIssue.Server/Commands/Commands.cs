using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Data.SqlClient;
using MyIssue.Server.Net;
using MyIssue.Server.Database;
using MyIssue.Server.Tools;

namespace MyIssue.Server.Commands
{
    public class Commands : IDBCommands, IUserCommands, ILogin
    {
        private readonly INetwork _net;
        private readonly IDBConnector _connector;
        private readonly IAdminQueries _aQueries;
        private readonly IUserQueries _uQueries;
        private readonly IStringTools _tools;
        private readonly SqlConnectionStringBuilder cString;
        public Commands()
        {
            _net = new Network();
            cString = DBParameters.ConnectionString;
            _connector = new DBConnector();
            _aQueries = new Queries();
            _uQueries = new Queries();
            _tools = new StringTools();

        }

        public void Login(Client client, CancellationToken ct)
        {

            _net.Write(client.ConnectedSock, "LOGGING IN\r\n", ct);
            LogUser.TypedCommand("Login", "User try to", client);
            client.CommandHistory.Add(_net.Receive(client.ConnectedSock, ct).Result);
            string[] task = _tools.CommandSplitter(client.CommandHistory[client.CommandHistory.Count - 1], "\r\n<NEXT>\r\n");
            var query = _uQueries.Login(task, DBParameters.Parameters.UsersTable);
            DataTable s = _connector.MakeReadQuery(cString, query);
            if (s.Rows[0][0].Equals(task[0]) && s.Rows[0][1].Equals(task[1]))
            {
                LogUser.TypedCommand("Login", "", client);
                client.Status = Convert.ToInt32(s.Rows[0][2]);
                _net.Write(client.ConnectedSock, "LOGGED!\r\n", ct);

            }
        }
        public void History(Client client, CancellationToken ct)
        {
            LogUser.TypedCommand("History", "Executed", client);
            string commandHistory = string.Join("\r\n", client.CommandHistory.ToArray()) + "\r\n";
            if (commandHistory.Length > 1024) commandHistory = commandHistory.Substring(0, 1019) + "\r\n";
            _net.Write(client.ConnectedSock, commandHistory, ct);
        }
        public void Disconnect(Client client, CancellationToken ct)
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                LogUser.TypedCommand("Disconnect", "Executed", client);
                client.Terminated = true;
                client.ConnectedSock.Close();
                client.ConnectedSock.Dispose();
                cts.Cancel();
            }


        }
        public void WhoAmI(Client client, CancellationToken ct)
        {
            LogUser.TypedCommand("WhoAmI", "Executed", client);
            string whoAreYou = string.Format("ID: {0}\r\nStatus: {1}\r\nLast Command: {2}\r\nAddress: {3}\r\n",
                client.Id, client.Status, client.CommandHistory[client.CommandHistory.Count - 1], client.ConnectedSock.RemoteEndPoint);
            _net.Write(client.ConnectedSock, whoAreYou, ct);
        }
        public void CreateTask(Client client, CancellationToken ct)
        {
            if (!client.Status.Equals(0))
            {
                LogUser.TypedCommand("CreateTask", "Executed", client);
                _net.Write(client.ConnectedSock, "CREATING TASK\r\n", ct);
                client.CommandHistory.Add(_net.Receive(client.ConnectedSock, ct).Result);
                var query = _uQueries.InsertNewTask(
                    _tools.CommandSplitter(client.CommandHistory[client.CommandHistory.Count - 1], "\r\n<NEXT>\r\n"),
                    "dbo.TASKS");
                _connector.MakeWriteQuery(cString, query);

            }
            else
            {
                LogUser.TypedCommand("CreateTask", "Tried to Execute", client);
                _net.Write(client.ConnectedSock, "NOT LOGGED IN\r\n", ct);
            }

        }
        public void AddEmployee(Client client, CancellationToken ct)
        {

            if (client.Status.Equals(2))
            {
                LogUser.TypedCommand("AddEmployee", "Executed", client);
                _net.Write(client.ConnectedSock, "ADD EMPLOYEE\r\n", ct);
                client.CommandHistory.Add(_net.Receive(client.ConnectedSock, ct).Result);
                string[] value = _tools.CommandSplitter(client.CommandHistory[client.CommandHistory.Count - 1], "\r\n<NEXT>\r\n");
                var employeequery = _aQueries.AddEmployee(value.Skip(3).ToArray(), DBParameters.Parameters.EmployeesTable);
                var userquery = _aQueries.AddUser(value.Take(3).ToArray(), DBParameters.Parameters.UsersTable);
                _connector.MakeWriteQuery(cString, userquery);
                _connector.MakeWriteQuery(cString, employeequery);
            }
            else
            {
                LogUser.TypedCommand("AddEmployee", "Tried to execute", client);
                _net.Write(client.ConnectedSock, "INSUFFICIENT PERMISSIONS!\r\n", ct);
            }
        }
        public void AddUser(Client client, CancellationToken ct)
        {

            if (client.Status.Equals(2))
            {
                LogUser.TypedCommand("AddUser", "Executed", client);
                _net.Write(client.ConnectedSock, "ADD USER\r\n", ct);
                client.CommandHistory.Add(_net.Receive(client.ConnectedSock, ct).Result);
                var query = _aQueries.AddUser(_tools.CommandSplitter(client.CommandHistory[client.CommandHistory.Count - 1], "\r\n<NEXT>\r\n")
                , DBParameters.Parameters.UsersTable);
                _connector.MakeWriteQuery(cString, query);
            }
            else
            {
                LogUser.TypedCommand("AddUser", "Tried to execute", client);
                _net.Write(client.ConnectedSock, "INSUFFICIENT PERMISSIONS!\r\n", ct);
            }
        }

    }

}
