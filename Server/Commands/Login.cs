using System;
using System.Net.Http;
using System.Threading;
using MyIssue.Server.Net;
using System.Security.Authentication;
using MyIssue.Infrastructure.Files;

namespace MyIssue.Server.Commands
{
    public class Login : Command
    {
        public Login() : base() { }
        public static string Name = "Login";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            NetWrite.Write(client.ConnectedSock, "LOGGING IN\r\n", ct);
            LogUser.TypedCommand("Login", "User try to", client);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            try
            {
                string[] input = SplitToCommand.Get(client.CommandHistory);
                client.Login = input[0];
                client.Password = input[1];
                using (var request = new HttpRequestMessage(HttpMethod.Get,
                    httpclient.BaseAddress + "api/Clients/"))
                {
                    var token = SetBearerToken(client.Login, client.Password);
                    HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        client.Token = token;
                        NetWrite.Write(client.ConnectedSock, token, ct);
                    }
                    else NetWrite.Write(client.ConnectedSock, "Unauthorized", ct);
                }
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
