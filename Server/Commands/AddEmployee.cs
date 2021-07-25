using MyIssue.Core.Entities;
using MyIssue.Core.Entities.Builders;
using MyIssue.Server.Net;
using System.Threading;

namespace MyIssue.Server.Commands
{
    class AddEmployee : Cmd
    {
        public override void Command(Client client, CancellationToken ct)
        {

            if (!client.Status.Equals(2)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("AddEmployee", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "ADD EMPLOYEE\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);

                
            var sqlCommand = SqlCommandInputBuilder
               .Create()
                   .SetCommandFromArray(client.CommandHistory)
                   .SetTable(DBParameters.Parameters.EmployeesTable)
               .Build();
            var employeequery = _sqlCommandParser.SqlCmdParser(this.GetType().Name, sqlCommand);
            _connector.MakeWriteQuery(cString, employeequery);

        }
    }
}
