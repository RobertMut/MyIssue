using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using User = MyIssue.Core.Commands.User;

namespace MyIssue.Web.Services
{
        public interface IEmployeesService
    {
        Task<string> GetEmployees(string? username, TokenAuth model);
        Task<string> CreateEmployee(EmployeeReturn employee, TokenAuth model);
    }
    public class EmployeesService : IEmployeesService
    {
        private readonly IServerConnector _server;

        public EmployeesService(IServerConnector server)
        {
            _server = server;
        }
        public async Task<string> GetEmployees(string? username, TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetEmployee\r\n<EOF>\r\n"));
            if (username is null) username = string.Empty;
            cmds = cmds.Append(
                StringStatic.ByteMessage($"{username}\r\n<EOF>\r\n"));

            cmds = cmds.Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            return response;
        }
        public async Task<string> CreateEmployee(EmployeeReturn employee, TokenAuth model)
        {
            string commandString =
                $"{employee.Login}\r\n<NEXT>\r\n{employee.Name}\r\n<NEXT>\r\n{employee.Surname}\r\n<NEXT>\r\n" +
                $"{employee.No}\r\n<NEXT>\r\n{employee.Position}\r\n<EOF>\r\n";
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("AddEmployee\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage(commandString))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            //Console.WriteLine(response);
            return response;
        }
    }
}
