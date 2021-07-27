using MyIssue.Core.Entities;
using MyIssue.Core.Entities.Database;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Net;
using System;
using System.Threading;

namespace MyIssue.Server.Commands
{
    public class CreateTask : Cmd
    {
        public override void Command(Client client, CancellationToken ct)
        {
            if (client.Status.Equals(0)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("CreateTask", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "CREATING TASK\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            try
            {
                var input = SplitToCommand.Get(client.CommandHistory);
                var clientId = unitOfWork.Client.GetClientByName(input[3]);
                unitOfWork.Task.InsertTask(input, clientId);
                unitOfWork.Complete();
            } catch (Exception e)
            {
                ExceptionHandler.HandleMyException(e);
            }

        }
    }
}
