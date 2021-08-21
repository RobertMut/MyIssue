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
            string[] splitted = SplitToCommand.Get(client.CommandHistory);
            string name = splitted[0];
            string surname = splitted[1];
            string no = splitted[2];
            decimal position = decimal.Parse(splitted[3]);
            string login = splitted[4];
            unit.EmployeeRepository.Add(new Employee
            {
                EmployeeName = name,
                EmployeeSurname = surname,
                EmployeeNo = no,
                EmployeePosition = position,
                EmployeeLogin = login,
            });
            unit.Complete();

        }
    }
}
