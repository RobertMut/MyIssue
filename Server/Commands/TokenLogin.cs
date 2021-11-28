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
                httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", input[1]);
                HttpResponseMessage httpResponse = httpclient.GetAsync("api/Clients/").Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    NetWrite.Write(client.ConnectedSock, "OK", ct);
                    client.Login = input[0];
                    client.Token = input[1];
                }

                else NetWrite.Write(client.ConnectedSock, "Unauthorized", ct);


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
