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
using MyIssue.Server.Net;

namespace MyIssue.Server.Commands
{
    public class GetFirstPagedTask : Command
    {
        public static string Name = "GetFirstPagedTask";

        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            //if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand(Name, "Executed", client);
            NetWrite.Write(client.ConnectedSock, "GET PAGED\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] input = SplitToCommand.Get(client.CommandHistory);
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            HttpResponseMessage httpResponse = httpclient.GetAsync($"api/Tasks/paged?pageNumber={input[0]}&pageSize={input[1]}").Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            NetWrite.Write(client.ConnectedSock, response, ct);
        }
    }
}
