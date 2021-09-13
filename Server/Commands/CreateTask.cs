using MyIssue.Infrastructure.Files;
using MyIssue.Server.Net;
using System;
using System.Linq;
using System.Threading;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Model;
using System.Threading.Tasks;

namespace MyIssue.Server.Commands
{
    public class CreateTask : Command
    {
        public static string Name = "CreateTask";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {
            if (!client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("CreateTask", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "CREATING TASK\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            try
            {
                string[] input = SplitToCommand.Get(client.CommandHistory);
                string title = input[0];
                string desc = input[1];
                DateTime date = DateTime.Parse(input[2]);
                string clientName = input[3];
                decimal type = decimal.Parse(input[4]);
                /*httpclient.Post<Model.Task>("api/Tasks", new Model.Task()
                {
                    TaskTitle = input[0],
                    TaskDescription = input[1],
                    TaskClient = input[2],
                    TaskAssignment = input[3],
                    TaskOwner = input[4],
                    TaskType = input[5],
                    TaskStart = DateTime.Parse(input[6]),
                    TaskEnd = DateTime.Parse(input[7]),
                    TaskCreationDate = DateTime.Parse(input[8]),
                    CreatedByMail = input[9]
                }).GetAwaiter().GetResult();*/
                //var clientId = unit.ClientRepository.Get(s => s.ClientName == clientName).Select(c => c.ClientId).FirstOrDefault();
                // unit.TaskRepository.Add(new Infrastructure.Database.Models.Task
                // {
                //     TaskTitle = title,
                //     TaskDesc = desc,
                //     TaskCreation = date,
                //     TaskClient = clientId,
                //     TaskType = type
                // });
                // unit.Complete();
            } catch (Exception e)
            {
                NetWrite.Write(client.ConnectedSock, "FAILED TO CREATE TASK\r\n", ct);
                LogUser.TypedCommand("CreateTask", "Failed to add new task!", client);
                SerilogLogger.ServerLogException(e);
            }

        }
    }
}
