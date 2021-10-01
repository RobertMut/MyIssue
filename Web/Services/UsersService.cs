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
    public interface IUsersService
    {
        Task<UsersRoot> GetUsers(string? username, TokenAuth model);
    }
    public class UsersService : IUsersService
    {
        private readonly IServerConnector _server;

        public UsersService(IServerConnector server)
        {
            _server = server;
        }
        public async Task<UsersRoot> GetUsers(string? username, TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetUser\r\n<EOF>\r\n"));
            Console.WriteLine("TOKEN   " + model.Token);
            if (username is null) username = string.Empty;
            cmds = cmds.Append(
                StringStatic.ByteMessage($"{username}\r\n<EOF>\r\n"));

            cmds = cmds.Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            var task = JsonConvert.DeserializeObject<UsersRoot>(response);
            return task;
        }
    }
}
