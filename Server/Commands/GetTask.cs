using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Model;
using MyIssue.Server.Net;

namespace MyIssue.Server.Commands
{
    public class GetTask : Command
    {
        public static string Name = "GetTask";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("Get", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "GET\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] input = SplitToCommand.Get(client.CommandHistory);
            if (input.Length.Equals(4)) input = input.Append("").ToArray();
            using (var request = new HttpRequestMessage(HttpMethod.Get,
                httpclient.BaseAddress + $"api/Tasks/filter/all={input[0]}&closed={input[1]}&whose={input[2]}&howmany={input[3]}/{input[4]}")) {
                Console.WriteLine(request.RequestUri.AbsoluteUri);
                Console.WriteLine(request.RequestUri.AbsolutePath);
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
