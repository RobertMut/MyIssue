using MyIssue.Core.Entities;
using MyIssue.Core.Entities.Builders;
using MyIssue.Server.Net;
using System.Threading;

namespace MyIssue.Server.Commands
{
    public class CreateTask : Cmd
    {
        public override void Command(Client client, CancellationToken ct)
        {
            if (!client.Status.Equals(0)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("CreateTask", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "CREATING TASK\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            var query = _sqlCommandParser.SqlCmdParser("InsertNewTask", SqlCommandInputBuilder
           .Create()
               .SetCommandFromArray(client.CommandHistory)
               .SetTable(DBParameters.Parameters.TaskTable)
           .Build());
            _connector.MakeWriteQuery(cString, query);
        }
    }
}
