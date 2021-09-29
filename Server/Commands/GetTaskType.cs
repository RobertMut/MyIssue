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
    public class GetTaskType : Command
    {
        public static string Name = "GetTaskType";

        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("GetTaskType", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "GET TASKTYPE\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            using (var request = new HttpRequestMessage(HttpMethod.Get,
                httpclient.BaseAddress + "api/TaskType/"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                request.Headers.Connection.Add("keep-alive");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
                HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
                string response = httpResponse.Content.ReadAsStringAsync().Result;
                NetWrite.Write(client.ConnectedSock, response, ct);
            }
        }
    }
}
