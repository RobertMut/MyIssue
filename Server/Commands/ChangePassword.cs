using MyIssue.Infrastructure.Files;
using MyIssue.Server.Net;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Model;
using System.Threading.Tasks;
using MyIssue.Core.String;
using Newtonsoft.Json;
using Task = MyIssue.Server.Model.Task;

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
            using (var request = new HttpRequestMessage(HttpMethod.Put,
                httpclient.BaseAddress + $"api/Users/put/"+client.Login))
            {
                var json = JsonConvert.SerializeObject(new
                {
                    UserLogin = client.Login,
                    OldPassword = input[0],
                    NewPassword = input[1]
                });
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                request.Headers.Connection.Add("keep-alive");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
                HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
                NetWrite.Write(client.ConnectedSock, httpResponse.StatusCode.ToString(), ct);
            }

        }
    }
}
