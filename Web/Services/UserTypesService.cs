using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Core.Commands;
using MyIssue.Core.Model.Return;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using Newtonsoft.Json;

namespace MyIssue.Web.Services
{
    public interface IUserTypesService
    {
        Task<string> GetUserTypes(TokenAuth model);
    }

    public class UserTypesService : IUserTypesService
    {
        private readonly IServerConnector _connector;

        public UserTypesService(IServerConnector connector)
        {
            _connector = connector;
        }

        public async Task<string> GetUserTypes(TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetUserTypes\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _connector.SendData(cmds);
            return response;
        }
    }
}
