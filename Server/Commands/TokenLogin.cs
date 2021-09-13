using System;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
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
                Console.WriteLine(input[0]);
                Console.WriteLine(input[1]);
                HttpResponseMessage httpresponse =
                    httpclient.GetAsync("api/Users/" + input[0]).GetAwaiter().GetResult();
                string response = httpresponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Console.WriteLine(response);
                var data = (JObject) JsonConvert.DeserializeObject(response);
                if (data["message"].Contains("incorrect")) throw new InvalidCredentialException("INCORRECT!\r\n");

                LogUser.TypedCommand("login", "", client);
                client.Status = Convert.ToInt32(data["type"]);
                client.Login = data["login"].ToString();
                NetWrite.Write(client.ConnectedSock, data["token"].ToString(), ct);

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
