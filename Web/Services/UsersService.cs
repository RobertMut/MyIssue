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
        Task<UserReturnRoot> GetUsers(string? username, TokenAuth model);
        Task<string> ChangePassword(Password password, TokenAuth model);
    }
    public class UsersService : IUsersService
    {
        private readonly IServerConnector _server;

        public UsersService(IServerConnector server)
        {
            _server = server;
        }
        public async Task<UserReturnRoot> GetUsers(string? username, TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetUser\r\n<EOF>\r\n"));
            //Console.WriteLine("TOKEN   " + model.Token);
            if (username is null) username = string.Empty;
            cmds = cmds.Append(
                StringStatic.ByteMessage($"{username}\r\n<EOF>\r\n"));

            cmds = cmds.Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            var task = JsonConvert.DeserializeObject<UserReturnRoot>(response);
            return task;
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
    }
}
