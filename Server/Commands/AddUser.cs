using MyIssue.Core.Entities;
using MyIssue.Core.Entities.Builders;
using MyIssue.Infrastructure.Database.Models;
using MyIssue.Server.Net;
using System.Threading;

namespace MyIssue.Server.Commands
{
    public class AddUser : Command
    {
        public static string Name = "AddUser";
        public override void Invoke(Core.Entities.Client client, CancellationToken ct)
        {

            if (!client.Status.Equals(2)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("AddUser", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "ADD USER\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] splitted = SplitToCommand.Get(client.CommandHistory);
            string login = splitted[0];
            string pass = splitted[1];
            decimal type = decimal.Parse(splitted[2]);
            unit.UserRepository.Add(new User
            {
                UserLogin = login,
                Password = pass,
                UserType = type
            });
            unit.Complete();

        }
    }
}
