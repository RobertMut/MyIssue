using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using MyIssue.Infrastructure.Files;
using MyIssue.Server.Model;
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
                Console.WriteLine(input[0]);
                Console.WriteLine(input[1]);
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(new AuthToken
                    {
                        Username = input[0],
                        Token = input[1]
                    }), Encoding.UTF8, "application/json"
                );
                HttpResponseMessage httpresponse =
                    httpclient.PostAsync("api/Users/tokenauthenticate", content).GetAwaiter().GetResult();
                string response = httpresponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Console.WriteLine("CHECK IF CONTAIN INVALID");
                if (response.Contains("invalid")) throw new InvalidCredentialException("INCORRECT\r\n");
                Console.WriteLine("DESERIALIZE");
                var data = (JObject) JsonConvert.DeserializeObject(response);
                string token = data.SelectToken("token").ToString();
                LogUser.TypedCommand("TokenLogin", "", client);
                if (!TestToken(token)) throw new InvalidCredentialException("TOKEN INVALID");
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

        private bool TestToken(string token)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get,
                httpclient.BaseAddress + $"api/Tasks/filter/true/false/0/"))
            {
                Console.WriteLine("URI " + request.RequestUri);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage httpresponse = httpclient.SendAsync(request).GetAwaiter().GetResult();
                string response = httpresponse.StatusCode.ToString();
                if (response.Contains("OK")) return true;
                return false;
            }
        }
    }
}
