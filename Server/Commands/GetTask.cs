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
using MyIssue.Server.Model;
using MyIssue.Server.Net;

namespace MyIssue.Server.Commands
{
    public class GetTask : Command
    {
        public static string Name = "GetTask";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            //if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand(Name, "Executed", client);
            NetWrite.Write(client.ConnectedSock, "GET TASK\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] input = SplitToCommand.Get(client.CommandHistory);
            if (input.Length.Equals(3)) input = input.Append("").ToArray();
            //using (var request = new HttpRequestMessage(HttpMethod.Get,
            //    httpclient.BaseAddress + $"api/Tasks/filter/closed={input[0]}&whose={input[1]}&howmany={input[2]}/{input[3]}"))
            //{
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            HttpResponseMessage httpResponse = httpclient.GetAsync($"api/Tasks/filter/closed={input[0]}&whose={input[1]}&howmany={input[2]}/{input[3]}").Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            NetWrite.Write(client.ConnectedSock, response, ct);
            //}
        }
    }
}
