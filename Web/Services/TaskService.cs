using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Core.Commands;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using Newtonsoft.Json;

namespace MyIssue.Web.Services
{
    public interface ITaskService
    {
        Task<string> CreateTask(TaskReturn task, TokenAuth model);
        Task<TaskReturnRoot> GetTasks(bool isClosed, string whoseTasks, int howMany, int? id, TokenAuth model);
        Task<bool> PutTask(TaskReturn task, TokenAuth model);
        Task<PageResponse<TaskReturn>> FirstPagedGet(int? pageNumber, int? pageSize, TokenAuth model);
        Task<PageResponse<TaskReturn>> PagedLinkGet(string link, TokenAuth model);
    }
    public class TaskService : ITaskService
    {
        private readonly IServerConnector _server;

        public TaskService(IServerConnector server)
        {
            _server = server;
        }


        public async Task<TaskReturnRoot> GetTasks(bool isClosed, string whoseTasks, int howMany, int? id, TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetTask\r\n<EOF>\r\n"));

            if (id is not null)
            {
                cmds = cmds.Append(
                    StringStatic.ByteMessage($"{isClosed}\r\n<NEXT>\r\n{whoseTasks}\r\n<NEXT>\r\n{howMany}\r\n<NEXT>\r\n{id}\r\n<EOF>\r\n"));
            }
            else
            {
                cmds = cmds.Append(
                    StringStatic.ByteMessage($"{isClosed}\r\n<NEXT>\r\n{whoseTasks}\r\n<NEXT>\r\n{howMany}\r\n<EOF>\r\n"));
            }
            cmds = cmds.Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            var task = JsonConvert.DeserializeObject<TaskReturnRoot>(response);
            return task;
        }

        public async Task<bool> PutTask(TaskReturn task, TokenAuth model)
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

        public async Task<string> CreateTask(TaskReturn task, TokenAuth model)
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
            //Console.WriteLine(response);
            if (response.Contains("CreatedAtAction")) return response;
            return "Something went wrong";
        }

        public async Task<PageResponse<TaskReturn>> FirstPagedGet(int? pageNumber, int? pageSize, TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetFirstPagedTask\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage($"{pageNumber.ToString() ?? "null"}\r\n<NEXT>\r\n{pageSize.ToString() ?? "null"}\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            var page = JsonConvert.DeserializeObject<PageResponse<TaskReturn>>(response);
            return page;
        }

        public async Task<PageResponse<TaskReturn>> PagedLinkGet(string link, TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetPagedByLink\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage($"{link}\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            var page = JsonConvert.DeserializeObject<PageResponse<TaskReturn>>(response);
            return page;
        }
    }
}
