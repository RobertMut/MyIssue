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
            unitOfWork.Employee.AddNewEmployee(SplitToCommand.Get(client.CommandHistory));
            unitOfWork.Complete();

        }
    }
}
