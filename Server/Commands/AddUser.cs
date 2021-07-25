using MyIssue.Core.Entities;
using MyIssue.Core.Entities.Builders;
using MyIssue.Server.Net;
using System.Threading;

namespace MyIssue.Server.Commands
{
    public class AddUser : Cmd
    {

        public override void Command(Client client, CancellationToken ct)
        {

            if (!client.Status.Equals(2)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("AddUser", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "ADD USER\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            var query = _sqlCommandParser.SqlCmdParser(this.GetType().Name, SqlCommandInputBuilder
            .Create()
                .SetCommandFromArray(client.CommandHistory)
                .SetTable(DBParameters.Parameters.UsersTable)
            .Build());
            _connector.MakeWriteQuery(cString, query);

        }
    }
}
