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
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Core.String;
using MyIssue.Server.Http;
using Newtonsoft.Json;

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
                var request = RequestMessage.NewRequest(
                    httpclient.BaseAddress + $"api/Clients/",
                    HttpMethod.Post, client.Token);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
                string response = httpResponse.Content.ReadAsStringAsync().Result;
                NetWrite.Write(client.ConnectedSock, response, ct);
            }
            catch (Exception e)
            {
                NetWrite.Write(client.ConnectedSock, "FAILED TO POST CLIENT\r\n", ct);
                LogUser.TypedCommand(Name, "Failed to add new client!", client);
                SerilogLogger.ServerLogException(e);
            }

        }
    }
}
