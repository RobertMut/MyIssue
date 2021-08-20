using MyIssue.Infrastructure.Database.Models;
using MyIssue.Server.Net;
using System.Threading;

namespace MyIssue.Server.Commands
{
    class AddEmployee : Command
    {
        public static string Name = "AddEmployee";
        public override void Invoke(Core.Entities.Client client, CancellationToken ct)
        {

            if (!client.Status.Equals(2)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("AddEmployee", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "ADD EMPLOYEE\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            var splitted = SplitToCommand.Get(client.CommandHistory);
            unit.EmployeeRepository.Add(new Employee
            {
                EmployeeName = splitted[0],
                EmployeeSurname = splitted[1],
                EmployeeNo = splitted[2],
                EmployeePosition = decimal.Parse(splitted[3]),
                EmployeeLogin = splitted[4],
            });
            unit.Complete();

        }
    }
}
