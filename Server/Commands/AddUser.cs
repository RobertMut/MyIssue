using MyIssue.Server.Net;
using System.Threading;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Model;

namespace MyIssue.Server.Commands
{
    public class AddUser : Command
    {
        public static string Name = "AddUser";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            if (!client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("AddUser", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "ADD USER\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] splitted = SplitToCommand.Get(client.CommandHistory);
            // httpclient.PostAsync("api/Users");
            // httpclient.Post("api/Users", new User
            // {
            //     UserLogin = splitted[0],
            //     Password = splitted[1],
            //     UserType = int.Parse(splitted[2])
            // }).GetAwaiter().GetResult();
            // unit.UserRepository.Add(new User
            // {
            //     UserLogin = login,
            //     Password = pass,
            //     UserType = type
            // });
            // unit.Complete();

        }
    }
}
