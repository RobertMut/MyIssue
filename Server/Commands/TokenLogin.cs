using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using MyIssue.Core.DataTransferObjects.Request;
using MyIssue.Infrastructure.Files;
using MyIssue.Server.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyIssue.Server.Commands
{

    public class TokenLogin : Command
    {
        public static string Name = "TokenLogin";

        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            NetWrite.Write(client.ConnectedSock, "TOKEN LOGGING IN\r\n", ct);
            LogUser.TypedCommand("TokenLogin", "User try to", client);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            try
            {
                string[] input = SplitToCommand.Get(client.CommandHistory);
                //Console.WriteLine(input[0]);
                //Console.WriteLine(input[1]);
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(new AuthTokenRequest
                    {
                        Username = input[0],
                        Token = input[1]
                    }), Encoding.UTF8, "application/json"
                );
                HttpResponseMessage httpresponse =
                    httpclient.PostAsync("api/Auth/tokenauthenticate", content).Result;
                string response = httpresponse.Content.ReadAsStringAsync().Result;
                if (response.Contains("invalid")) throw new InvalidCredentialException("INCORRECT\r\n");
                var data = (JObject) JsonConvert.DeserializeObject(response);
                LogUser.TypedCommand("TokenLogin", "", client);
                client.Status = Convert.ToInt32(data.SelectToken("type"));
                client.Login = data.SelectToken("login").ToString();
                client.Token = data.SelectToken("token").ToString();
                NetWrite.Write(client.ConnectedSock, "CORRECT", ct);


            }
            catch (InvalidCredentialException e)
            {
                SerilogLogger.ServerLogException(e);
                LogUser.TypedCommand("Login", "User failed to login", client);
                NetWrite.Write(client.ConnectedSock, "INVALID", ct);
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
