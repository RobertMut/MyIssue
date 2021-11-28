using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Infrastructure.Server;

namespace MyIssue.Web.Services
{
    public interface IUserService
    {
        Task<string> GenerateToken(string login, string password);
        Task<bool> ValidateToken(string login, string token);
        Task<string> RevokeToken(string token);
    }
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
            if (response is null || response.Contains("Unauthorized")) return null;
            //Console.WriteLine(response);
            return response;
        }



        public async Task<bool> ValidateToken(string login, string token)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(Core.Commands.User.TokenLogin(login, token))
                .Concat(Core.Commands.User.Logout());
            string response = _server.SendData(cmds);
            if (!response.Contains("Unauthorized")) return true;
            return false;
        }
        public async Task<string> RevokeToken(string token)
        {
            //Console.WriteLine(nameof(this.RevokeToken));
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(Core.Commands.User.RevokeLogout(token));
            string response = _server.SendData(cmds);
            return response;
        }
    }
}
