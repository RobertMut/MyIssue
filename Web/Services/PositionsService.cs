using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Core.Commands;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using Newtonsoft.Json;

namespace MyIssue.Web.Services
{
    public interface IPositionsService
    {
        Task<string> GetEmployeePositons(TokenAuth model);
    }

    public class PositionsService : IPositionsService
    {
        private readonly IServerConnector _connector;

        public PositionsService(IServerConnector connector)
        { 
            _connector = connector;
        }
        public async Task<string> GetEmployeePositons(TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetPositions\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _connector.SendData(cmds);
            return response;
        }

    }
}
