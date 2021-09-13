using System;
using MyIssue.Server.Net;
using System.Threading;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Model;

namespace MyIssue.Server.Commands
{
    class AddEmployee : Command
    {
        public static string Name = "AddEmployee";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            if (!client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("AddEmployee", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "ADD EMPLOYEE\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] splitted = SplitToCommand.Get(client.CommandHistory);
            string name = splitted[0];
            string surname = splitted[1];
            string no = splitted[2];
            decimal position = decimal.Parse(splitted[3]);
            string login = splitted[4];
            // httpclient.Post("api/Employees", new Employee
            // {
            //     EmployeeLogin = splitted[0],
            //     EmployeeName = splitted[1],
            //     EmployeeSurname = splitted[2],
            //     EmployeeNo = splitted[3],
            //     EmployeePosition = Convert.ToInt32(splitted[4])
            // }).GetAwaiter().GetResult();
            /*
                unit.EmployeeRepository.Add(new Employee
            {
                EmployeeName = name,
                EmployeeSurname = surname,
                EmployeeNo = no,
                EmployeePosition = position,
                EmployeeLogin = login,
            });
            unit.Complete();*/

        }
    }
}
