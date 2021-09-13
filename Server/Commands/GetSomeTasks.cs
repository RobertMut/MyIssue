using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Model;
using MyIssue.Server.Net;

namespace MyIssue.Server.Commands
{
    public class GetSomeTasks : Command
    {
        public static string Name = "GetSomeTasks";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            if (!client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("GetSomeTasks", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "GET\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] command = SplitToCommand.Get(client.CommandHistory);
            int initial = Convert.ToInt32(command[0]);
            int end = initial + Convert.ToInt32(command[1]);
            HttpResponseMessage httpresponse = httpclient.GetAsync("api/Tasks/"+initial+"-"+end).GetAwaiter().GetResult();
            string response = httpresponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine(response);
            NetWrite.Write(client.ConnectedSock,response,ct);
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
