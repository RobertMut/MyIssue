using MyIssue.Core.Entities;
using System;
using System.Threading;
using MyIssue.Server.Net;
using System.Data;
using System.Linq;
using MyIssue.Core.Entities.Builders;
using MyIssue.Core.Exceptions;

namespace MyIssue.Server.Commands
{
    public class Login : Command
    {
        public static string Name { get { return "Login"; } }
        public override void Invoke(Client client, CancellationToken ct)
        {

            NetWrite.Write(client.ConnectedSock, "LOGGING IN\r\n", ct);
            LogUser.TypedCommand("Login", "User try to", client);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            try
            {
                var input = SplitToCommand.Get(client.CommandHistory);
                var query = unit.UserRepository.Get(log => log.UserLogin == input[0] && log.Password == input[1]).Select(s => s.UserType).FirstOrDefault();
                if (!(query is default(decimal)))
                {
                    LogUser.TypedCommand("Login", "", client);
                    client.Status = Convert.ToInt32(query);
                    NetWrite.Write(client.ConnectedSock, "LOGGED!\r\n", ct);
                }
            } catch (Exception e)
            {
                ExceptionHandler.HandleMyException(e);
            }

            
        }
    }
}
