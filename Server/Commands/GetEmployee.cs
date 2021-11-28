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
    public class GetEmployee : Command
    {
        public static string Name = "GetEmployee";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            //if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("GetEmployee", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "GET EMPLOYEE\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] input = SplitToCommand.Get(client.CommandHistory);
            string baseAddress = httpclient.BaseAddress + "api/Employees/";
            if (!input.Length.Equals(0)) baseAddress += input[0];
            var request = RequestMessage.NewRequest(
                baseAddress,
                HttpMethod.Get, client.Token);
            HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            NetWrite.Write(client.ConnectedSock, response, ct);
        }
    }
}
