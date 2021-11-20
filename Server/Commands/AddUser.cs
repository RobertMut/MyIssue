using System.Net.Http;
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
    public class AddUser : Command
    {
        public static string Name = "AddUser";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("AddUser", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "ADD USER\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] splitted = SplitToCommand.Get(client.CommandHistory);
            var json = JsonConvert.SerializeObject(new UserReturn
            {
                Username = splitted[0],
                Password = splitted[1],
                Type = splitted[2]
            });
            //var request =
            //    RequestMessage.NewRequest(httpclient.BaseAddress + "api/Users", HttpMethod.Post, client.Token);
            //request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
            //string response = httpResponse.Content.ReadAsStringAsync().Result;
            //NetWrite.Write(client.ConnectedSock, response, ct);

        }
    }
}
