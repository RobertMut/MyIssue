using MyIssue.Core.Entities;
using System;
using MyIssue.Core.String;
using System.Threading;
using MyIssue.Server.Net;
using System.Data;
using MyIssue.Core.Entities.Builders;

namespace MyIssue.Server.Commands
{
    public class Login : Cmd
    {
        public override void Command(Client client, CancellationToken ct)
        {

            NetWrite.Write(client.ConnectedSock, "LOGGING IN\r\n", ct);
            LogUser.TypedCommand("Login", "User try to", client);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            var cmdInput = SqlCommandInputBuilder
               .Create()
                   .SetCommandFromArray(client.CommandHistory)
                   .SetTable(DBParameters.Parameters.UsersTable)
               .Build();
            var query = _sqlCommandParser.SqlCmdParser(this.GetType().Name, cmdInput);
            DataTable s = _connector.MakeReadQuery(cString, query);
            if (s.Rows[0][0].Equals(cmdInput.Command[0]) && s.Rows[0][1].Equals(cmdInput.Command[1]))
            {
                LogUser.TypedCommand("Login", "", client);
                client.Status = Convert.ToInt32(s.Rows[0][2]);
                NetWrite.Write(client.ConnectedSock, "LOGGED!\r\n", ct);

            }
        }
    }
}
