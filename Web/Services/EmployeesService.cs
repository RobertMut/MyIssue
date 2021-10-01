using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using Newtonsoft.Json;
using User = MyIssue.Core.Commands.User;

namespace MyIssue.Web.Services
{
        public interface IEmployeesService
    {
        Task<EmployeeBasicRoot> GetEmployees(string? username, TokenAuth model);
    }
    public class EmployeesService : IEmployeesService
    {
        private readonly IServerConnector _server;

        public EmployeesService(IServerConnector server)
        {
            _server = server;
        }
        public async Task<EmployeeBasicRoot> GetEmployees(string? username, TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetEmployee\r\n<EOF>\r\n"));
            if (username is null) username = string.Empty;
            cmds = cmds.Append(
                StringStatic.ByteMessage($"{username}\r\n<EOF>\r\n"));

            cmds = cmds.Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            var employee = JsonConvert.DeserializeObject<EmployeeBasicRoot>(response);
            return employee;
        }
    }
}
