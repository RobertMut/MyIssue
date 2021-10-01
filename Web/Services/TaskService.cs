using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Helpers;
using MyIssue.Web.Model;
using Newtonsoft.Json;
using DateTime = System.DateTime;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Task = MyIssue.Web.Model.Task;
using User = MyIssue.Core.Commands.User;

namespace MyIssue.Web.Services
{
    public interface ITaskService
    {
        Task<string> CreateTask(Task task, TokenAuth model);
        Task<TaskRoot> GetTasks(bool isClosed, bool all, string whoseTasks, int howMany, int? id, TokenAuth model);
        Task<bool> PutTask(Task task, TokenAuth model);
    }
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
            var task = JsonConvert.DeserializeObject<TaskRoot>(response);
            return task;
        }

        public async Task<bool> PutTask(Task task, TokenAuth model)
        {
            string commandString =
                $"{task.TaskId}\r\n<NEXT>\r\n{task.TaskTitle}\r\n<NEXT>\r\n{task.TaskDescription}\r\n<NEXT>\r\n" +
                $"{task.TaskClient}\r\n<NEXT>\r\n{task.TaskAssignment ?? "null"}\r\n<NEXT>\r\n{task.TaskOwner ?? "null"}\r\n<NEXT>\r\n" +
                $"{task.TaskType}\r\n<NEXT>\r\n{task.TaskStart}\r\n<NEXT>\r\n{task.TaskEnd}\r\n<NEXT>\r\n" +
                $"{task.TaskCreationDate}\r\n<NEXT>\r\n{task.CreatedByMail}\r\n<NEXT>\r\n" +
                $"{task.EmployeeDescription ?? "null"}\r\n<EOF>\r\n";
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("PutTask\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage(commandString))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            if (response.Equals("NoContent")) return true;
            return false;
        }

        public async Task<string> CreateTask(Task task, TokenAuth model)
        {
            string commandString =
                $"{task.TaskTitle}\r\n<NEXT>\r\n{task.TaskDescription}\r\n<NEXT>\r\n{task.TaskClient}\r\n<NEXT>\r\n" +
                $"{task.TaskAssignment ?? "null"}\r\n<NEXT>\r\n{task.TaskOwner ?? "null"}\r\n<NEXT>\r\n{task.TaskType}\r\n<NEXT>\r\n" +
                $"{task.TaskStart}\r\n<NEXT>\r\n{task.TaskEnd}\r\n<NEXT>\r\n" +
                $"{task.EmployeeDescription ?? "null"}\r\n<EOF>\r\n";
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("CreateTask\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage(commandString))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            Console.WriteLine(response);
            if (response.Contains("CreatedAtAction")) return response;
            return "Something went wrong";
        }
    }
}
