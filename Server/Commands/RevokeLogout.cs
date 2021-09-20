using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Infrastructure.Files;
using MyIssue.Server.Model;
using MyIssue.Server.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyIssue.Server.Commands
{
    public class RevokeLogout : Command
    {
        public static string Name = "RevokeLogout";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            NetWrite.Write(client.ConnectedSock, "REVOKE LOGOUT\r\n", ct);
            LogUser.TypedCommand("RevokeLogout", "User try to", client);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            try
            {
                string input = SplitToCommand.Get(client.CommandHistory)[0];
                Console.WriteLine(input[0]);
                Console.WriteLine(input[1]);
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(new Token
                    {
                        TokenString = input
                    }), Encoding.UTF8, "application/json"
                    );
                HttpResponseMessage httpresponse =
                    httpclient.PostAsync("api/Users/logout", content).GetAwaiter().GetResult();
                string response = httpresponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if(response.Contains("Bad Request")) NetWrite.Write(client.ConnectedSock, "Bad request", ct);
                else
                {
                    NetWrite.Write(client.ConnectedSock, response, ct);
                    LogUser.TypedCommand("RevokeLogout", "Executed", client);
                    client.Terminated = true;
                    client.ConnectedSock.Close();
                    client.ConnectedSock.Dispose();
                    CancellationTokenSource.CreateLinkedTokenSource(ct).Cancel();
                }

            }
            catch (NullReferenceException nullReference)
            {
                SerilogLogger.ServerLogException(nullReference);
                LogUser.TypedCommand("RevokeLogout", "Exception occured!", client);
                NetWrite.Write(client.ConnectedSock, nullReference.Message, ct);
            }
        }
    }
}
