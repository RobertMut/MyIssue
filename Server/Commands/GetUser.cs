using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Http;
using MyIssue.Server.Model;
using MyIssue.Server.Net;

namespace MyIssue.Server.Commands
{
    public class GetUser : Command
    {
        public static string Name = "GetUser";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            //if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("GetUser", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "GET USER\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] input = SplitToCommand.Get(client.CommandHistory);
            var request = RequestMessage.NewRequest(httpclient.BaseAddress+ $"api/Users/{input[0] ?? string.Empty}",
                HttpMethod.Get, client.Token);//$"https://127.0.0.1:6001/User/{ input[0] ?? string.Empty }"
            HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            NetWrite.Write(client.ConnectedSock, response, ct);


        }
    }
}
