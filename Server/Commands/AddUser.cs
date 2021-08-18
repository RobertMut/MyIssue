using MyIssue.Core.Entities;
using MyIssue.Core.Entities.Builders;
using MyIssue.Infrastructure.Database.Models;
using MyIssue.Server.Net;
using System.Threading;

namespace MyIssue.Server.Commands
{
    public class AddUser : Command
    {
        public static string Name { get { return "AddUser"; } }
        public override void Invoke(Core.Entities.Client client, CancellationToken ct)
        {

            if (!client.Status.Equals(2)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("AddUser", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "ADD USER\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            var splitted = SplitToCommand.Get(client.CommandHistory);
            unit.UserRepository.Add(new User
            {
                UserLogin = splitted[0],
                Password = splitted[1],
                UserType = decimal.Parse(splitted[2])
            });
            unit.Complete();

        }
    }
}
