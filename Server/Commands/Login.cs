using System;
using System.Threading;
using MyIssue.Server.Net;
using System.Linq;
using System.Net.Http;
using MyIssue.Infrastructure.Files;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyIssue.Server.Commands
{
    public class Login : Command
    {
        public static string Name = "Login";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            NetWrite.Write(client.ConnectedSock, "LOGGING IN\r\n", ct);
            LogUser.TypedCommand("Login", "User try to", client);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            try
            {
                string[] input = SplitToCommand.Get(client.CommandHistory);
                Console.WriteLine(input[0]);
                Console.WriteLine(input[1]);
                HttpResponseMessage httpresponse = httpclient.GetAsync("api/Users/" + input[0]).GetAwaiter().GetResult();
                string response = httpresponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Console.WriteLine(response);
                var data = (JObject) JsonConvert.DeserializeObject(response);
                if (input[1].Equals(data["password"]))
                {
                    LogUser.TypedCommand("Login", "", client);
                    client.Status = Convert.ToInt32(data["userType"]);
                    client.Login = data["userLogin"].ToString();
                    NetWrite.Write(client.ConnectedSock, "LOGGED "+Guid.NewGuid(), ct); //move guid to api
                }
                else
                {
                    LogUser.TypedCommand("Login", "User failed to login", client);
                    NetWrite.Write(client.ConnectedSock, "INCORRECT!\r\n", ct);
                }
            } catch (Exception e)
            {
                SerilogLogger.ServerLogException(e);
            }

            
        }
    }
}
