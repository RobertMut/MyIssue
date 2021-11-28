using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MyIssue.Server.Net;
using System.Threading;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Http;
using MyIssue.Server.Model;
using Newtonsoft.Json;

namespace MyIssue.Server.Commands
{
    class AddEmployee : Command
    {
        public static string Name = "AddEmployee";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("AddEmployee", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "ADD EMPLOYEE\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] splitted = SplitToCommand.Get(client.CommandHistory);
            var json = JsonConvert.SerializeObject(new EmployeeReturn
            {
                Login = splitted[0],
                Name = splitted[1],
                Surname = splitted[2],
                No = splitted[3],
                Position = splitted[4]
            });
            using (var request = new HttpRequestMessage(HttpMethod.Post,
                httpclient.BaseAddress + "api/Employees"))
            {
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
                HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
                string response = httpResponse.Content.ReadAsStringAsync().Result;
                NetWrite.Write(client.ConnectedSock, response, ct);
            }

        }
    }
}
