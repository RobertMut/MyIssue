using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Core.Model.Request;
using MyIssue.Core.Model.Return;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using Newtonsoft.Json;
using User = MyIssue.Core.Commands.User;

namespace MyIssue.Web.Services
{
    public interface IUsersService
    {
        Task<string> GetUsers(string? username, TokenAuth model);
        Task<string> ChangePassword(Password password, TokenAuth model);
        Task<string> CreateUser(UserReturn user, TokenAuth model);
    }
    public class UsersService : IUsersService
    {
        private readonly IServerConnector _server;

        public UsersService(IServerConnector server)
        {
            _server = server;
        }
        public async Task<string> GetUsers(string? username, TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetUser\r\n<EOF>\r\n"));
            //Console.WriteLine("TOKEN   " + model.Token);
            if (username is null) username = string.Empty;
            cmds = cmds.Append(
                StringStatic.ByteMessage($"{username}\r\n<EOF>\r\n"));

            cmds = cmds.Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            return _server.SendData(cmds);
        }

        public async Task<string> ChangePassword(Password password, TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("ChangePassword\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage(
                    $"{password.OldPassword}\r\n<NEXT>\r\n{password.NewPassword}\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            return _server.SendData(cmds);
        }
        public async Task<string> CreateUser(UserReturn user, TokenAuth model)
        {
            string commandString =
                $"{user.Username}\r\n<NEXT>\r\n{user.Password}\r\n<NEXT>\r\n{user.Type}\r\n<EOF>\r\n";
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("AddUser\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage(commandString))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            return response;
        }
    }
}
