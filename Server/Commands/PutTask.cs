using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Core.Exceptions;
using MyIssue.Core.String;
using MyIssue.Server.Model;
using MyIssue.Server.Net;
using Newtonsoft.Json;


namespace MyIssue.Server.Commands
{
    public class PutTask : Command
    {
        public static string Name = "PutTask";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("PutTask", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "PUT\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] input = SplitToCommand.Get(client.CommandHistory);

            var json = JsonConvert.SerializeObject(new TaskReturn()
            {
                TaskId = Convert.ToDecimal(input[0]),
                TaskTitle = input[1],
                TaskDescription = input[2],
                TaskClient = input[3],
                TaskAssignment = input[4],
                TaskOwner = input[5],
                TaskType = input[6],
                TaskStart = StringStatic.CheckDate(input[7]),
                TaskEnd = StringStatic.CheckDate(input[8]),
                TaskCreationDate = Convert.ToDateTime(input[9]),
                CreatedByMail = input[10],
                EmployeeDescription = input[11]
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
            HttpResponseMessage httpResponse = httpclient.PutAsync($"api/Tasks/{input[0]}", content).Result;
            NetWrite.Write(client.ConnectedSock, httpResponse.StatusCode.ToString(), ct);
        }



    }
}
