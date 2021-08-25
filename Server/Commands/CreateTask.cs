using MyIssue.Infrastructure.Files;
using MyIssue.Server.Net;
using System;
using System.Linq;
using System.Threading;
using MyIssue.Core.Exceptions;

namespace MyIssue.Server.Commands
{
    public class CreateTask : Command
    {
        public static string Name = "CreateTask";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {
            if (client.Status.Equals(0)) throw new NotSufficientPermissionsException();
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
                var clientId = unit.ClientRepository.Get(s => s.ClientName == clientName).Select(c => c.ClientId).FirstOrDefault();
                unit.TaskRepository.Add(new Infrastructure.Database.Models.Task
                {
                    TaskTitle = title,
                    TaskDesc = desc,
                    TaskCreation = date,
                    TaskClient = clientId,
                    TaskType = type
                });
                unit.Complete();
            } catch (Exception e)
            {
                NetWrite.Write(client.ConnectedSock, "FAILED TO CREATE TASK\r\n", ct);
                LogUser.TypedCommand("CreateTask", "Failed to add new task!", client);
                SerilogLogger.ServerLogException(e);
            }

        }
    }
}
