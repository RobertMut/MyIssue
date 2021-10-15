using System;
using System.Threading;
using MyIssue.Server.Net;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using MyIssue.Core.Model.Request;
using MyIssue.Infrastructure.Files;
using MyIssue.Core.Model.Return;
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
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(new AuthRequest
                    {
                        Username = input[0],
                        Password = input[1]
                    }), Encoding.UTF8, "application/json"
                    );
                HttpResponseMessage httpresponse =
                    httpclient.PostAsync("api/Auth/authenticate", content).Result;
                string response = httpresponse.Content.ReadAsStringAsync().Result;
                var data = (JObject) JsonConvert.DeserializeObject(response);
                if (!(data["message"] is null)) throw new InvalidCredentialException("INCORRECT\r\n");

                LogUser.TypedCommand("login", "", client);
                client.Status = Convert.ToInt32(data.SelectToken("type"));
                client.Login = data.SelectToken("login").ToString();
                client.Token = data.SelectToken("token").ToString();
                NetWrite.Write(client.ConnectedSock, response, ct);

            }
            catch (InvalidCredentialException e)
            {
                SerilogLogger.ServerLogException(e);
                LogUser.TypedCommand("Login", "User failed to login", client);
                NetWrite.Write(client.ConnectedSock, e.Message, ct);
            }
            catch (NullReferenceException nullReference)
            {
                SerilogLogger.ServerLogException(nullReference);
                LogUser.TypedCommand("Login", "Exception occured!", client);
                NetWrite.Write(client.ConnectedSock, nullReference.Message, ct);
            }
        }
    }
}
