using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using Newtonsoft.Json;

namespace MyIssue.Web.Services
{
    public interface ITaskTypesService
    {
        Task<TaskTypeReturnRoot> GetTaskTypes(TokenAuth model);
    }

    public class TaskTypesService : ITaskTypesService
    {
        private readonly IServerConnector _server;

        public TaskTypesService(IServerConnector server)
        {
            _server = server;
        }
        public async Task<TaskTypeReturnRoot> GetTaskTypes(TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(Core.Commands.User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetTaskType\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            cmds = cmds.Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            var tasktypes = JsonConvert.DeserializeObject<TaskTypeReturnRoot>(response);
            return tasktypes;
        }
    }
}
