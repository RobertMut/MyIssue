using MyIssue.Core.Entities;
using MyIssue.Infrastructure.Files;
using MyIssue.Server.Net;
using System;
using System.Linq;
using System.Threading;

namespace MyIssue.Server.Commands
{
    public class CreateTask : Command
    {
        public static string Name = "CreateTask";
        public override void Invoke(Client client, CancellationToken ct)
        {
            if (client.Status.Equals(0)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("CreateTask", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "CREATING TASK\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            try
            {
                var input = SplitToCommand.Get(client.CommandHistory);
                var clientId = unit.ClientRepository.Get(s => s.ClientName == input[3]).Select(c => c.ClientId).FirstOrDefault();
                unit.TaskRepository.Add(new Infrastructure.Database.Models.Task
                {
                    TaskTitle = input[0],
                    TaskDesc = input[1],
                    TaskCreation = DateTime.Parse(input[2]),
                    TaskClient = clientId,
                    TaskType = decimal.Parse(input[4])
                });
                unit.Complete();
            } catch (Exception e)
            {
                SerilogLogger.ServerLogException(e);
            }

        }
    }
}
