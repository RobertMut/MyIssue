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
using MyIssue.Core.Model.Return;

namespace MyIssue.Server.Commands
{
    public class PostClient : Command
    {
        public static string Name = "PostClient";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {
            if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand(Name, "Executed", client);
            NetWrite.Write(client.ConnectedSock, "POSTING CLIENT\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            try
            {
                string[] input = SplitToCommand.Get(client.CommandHistory);
                using (var request = new HttpRequestMessage(HttpMethod.Post,
                    httpclient.BaseAddress + $"api/Clients/"))
                {
                    var json = JsonConvert.SerializeObject(new ClientReturn
                    {
                        Id = null,
                        Name = input[0],
                        Country = input[1],
                        No = input[2],
                        Street = input[3],
                        StreetNo = input[4],
                        FlatNo = input[5],
                        Description = input[6]
                    });
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                    request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                    request.Headers.Connection.Add("keep-alive");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
                    HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
                    string response = httpResponse.Content.ReadAsStringAsync().Result;
                    NetWrite.Write(client.ConnectedSock, response, ct);
                }
            } catch (Exception e)
            {
                NetWrite.Write(client.ConnectedSock, "FAILED TO POST CLIENT\r\n", ct);
                LogUser.TypedCommand(Name, "Failed to add new client!", client);
                SerilogLogger.ServerLogException(e);
            }

        }
    }
}
