using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Net;

namespace MyIssue.Server.Commands
{
    public class GetClients : Command
    {
        public static string Name = "GetClients";

        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            //if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("GetClients", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "GET CLIENTS\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            HttpResponseMessage httpResponse = httpclient.GetAsync("api/Clients/").Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            NetWrite.Write(client.ConnectedSock, response, ct);
        }
    }
}
