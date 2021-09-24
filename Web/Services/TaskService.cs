using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Helpers;
using MyIssue.Web.Model;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Task = MyIssue.Web.Model.Task;
using User = MyIssue.Core.Commands.User;

namespace MyIssue.Web.Services
{
    public class TaskService : ITaskService
    {
        private readonly IServerConnector _server;

        public TaskService(IServerConnector server)
        {
            _server = server;
        }


        public async Task<TaskRoot> GetTasks(bool isClosed, bool all, string whoseTasks, int howMany, int? id, TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetTask\r\n<EOF>\r\n"));

            if (id is not null)
            {
                cmds = cmds.Append(
                    StringStatic.ByteMessage($"{all}\r\n<NEXT>\r\n{isClosed}\r\n<NEXT>\r\n{whoseTasks}\r\n<NEXT>\r\n{howMany}\r\n<NEXT>\r\n{id}\r\n<EOF>\r\n"));
            }
            else
            {
                cmds = cmds.Append(
                    StringStatic.ByteMessage($"{all}\r\n<NEXT>\r\n{isClosed}\r\n<NEXT>\r\n{whoseTasks}\r\n<NEXT>\r\n{howMany}\r\n<EOF>\r\n"));
            }
            cmds = cmds.Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            Console.WriteLine($"RESPONSE: {response}");
            Console.WriteLine("deserialization");
            var task = JsonConvert.DeserializeObject<TaskRoot>(response);
            return task;
        }
    }
}
