using MyIssue.Infrastructure.Files;
using MyIssue.Server.Net;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using MyIssue.Core.Exceptions;
using System.Threading.Tasks;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Core.String;
using Newtonsoft.Json;

namespace MyIssue.Server.Commands
{
    public class CreateTask : Command
    {
        public static string Name = "CreateTask";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {
            if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("CreateTask", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "CREATING TASK\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            try
            {
                string[] input = SplitToCommand.Get(client.CommandHistory);

                var json = JsonConvert.SerializeObject(new TaskReturn
                {
                    TaskTitle = input[0],
                    TaskDescription = input[1],
                    TaskClient = input[2],
                    TaskAssignment = input[3],
                    TaskOwner = input[4],
                    TaskType = input[5],
                    TaskStart = StringStatic.CheckDate(input[6]),
                    TaskEnd = StringStatic.CheckDate(input[7]),
                    EmployeeDescription = input[8]
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
                HttpResponseMessage httpResponse = httpclient.PostAsync($"api/Tasks/", content).Result;
                NetWrite.Write(client.ConnectedSock, httpResponse.StatusCode.ToString(), ct);
            }
            catch (Exception e)
            {
                NetWrite.Write(client.ConnectedSock, "FAILED TO CREATE TASK\r\n", ct);
                LogUser.TypedCommand("CreateTask", "Failed to add new task!", client);
                SerilogLogger.ServerLogException(e);
            }

        }
    }
}
