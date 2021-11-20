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
    public class GetPositions : Command
    {
        public static string Name = "GetPositions";

        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            //if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            //LogUser.TypedCommand(Name, "Executed", client);
            //NetWrite.Write(client.ConnectedSock, $"GET {Name}\r\n", ct);
            //client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            //var request =
            //    RequestMessage.NewRequest(httpclient.BaseAddress + "api/Positions", HttpMethod.Get, client.Token);
            //HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
            //string response = httpResponse.Content.ReadAsStringAsync().Result;
            //NetWrite.Write(client.ConnectedSock, response, ct);
        }
    }
}
