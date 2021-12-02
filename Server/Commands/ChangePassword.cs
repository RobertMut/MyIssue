using System;
using MyIssue.Server.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Http;
using MyIssue.Server.Model;
using Newtonsoft.Json;

namespace MyIssue.Server.Commands
{
    public class ChangePassword : Command
    {
        public static string Name = "ChangePassword";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {
            if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand(Name, "Executed", client);
            NetWrite.Write(client.ConnectedSock, "CHANGING PASSWORD\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] input = SplitToCommand.Get(client.CommandHistory);

            var json = JsonConvert.SerializeObject(new
            {
                UserLogin = client.Login,
                OldPassword = input[0],
                NewPassword = input[1]
            });
            var request = RequestMessage.NewRequest(
                httpclient.BaseAddress + "api/Users/" + client.Login,
                HttpMethod.Put, client.Token);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
            NetWrite.Write(client.ConnectedSock, httpResponse.StatusCode.ToString(), ct);
        }

    }
}
