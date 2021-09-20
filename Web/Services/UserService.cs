using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using Task = MyIssue.Web.Model.Task;

namespace MyIssue.Web.Services
{
    public class UserService : IUserService
    {
        private readonly IServerConnector _server;

        public UserService(IServerConnector server)
        {
            _server = server;
        }

        public async Task<string> GenerateToken(string login, string password)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(Core.Commands.User.Login(login, password))
                .Concat(Core.Commands.User.Logout());
            string response = _server.SendData(cmds);
            if (response is null || response.Contains("INCORRECT")) return null;
            Console.WriteLine(response);
            return response;
        }



        public async Task<bool> ValidateToken(string login, string token)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(Core.Commands.User.TokenLogin(login, token))
                .Concat(Core.Commands.User.Logout());
            string response = _server.SendData(cmds);
            if (response.Contains("CORRECT")) return true;
            return false;
        }
        public async Task<string> RevokeToken(string token)
        {
            Console.WriteLine(nameof(this.RevokeToken));
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(Core.Commands.User.RevokeLogout(token));
            string response = _server.SendData(cmds);
            return response;
        }
    }
}
