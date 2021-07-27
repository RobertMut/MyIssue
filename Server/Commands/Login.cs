using MyIssue.Core.Entities;
using System;
using System.Threading;
using MyIssue.Server.Net;
using System.Data;
using MyIssue.Core.Entities.Builders;

namespace MyIssue.Server.Commands
{
    public class Login : Cmd
    {
        public override void Command(Client client, CancellationToken ct)
        {

            NetWrite.Write(client.ConnectedSock, "LOGGING IN\r\n", ct);
            LogUser.TypedCommand("Login", "User try to", client);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            var query = unitOfWork.User.TypeLogin(SplitToCommand.Get(client.CommandHistory));
            if (!(query is null))
            {
                LogUser.TypedCommand("Login", "", client);
                client.Status = Convert.ToInt32(query);
                NetWrite.Write(client.ConnectedSock, "LOGGED!\r\n", ct);
            }
            
        }
    }
}
