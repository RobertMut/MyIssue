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
    public interface IClientsService
    {
        Task<ClientNameRoot> GetClient(TokenAuth model);
    }

    public class ClientsService : IClientsService
    {
        private readonly IServerConnector _server;

        public ClientsService(IServerConnector server)
        {
            _server = server;
        }

        public async Task<ClientNameRoot> GetClient(TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(Core.Commands.User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetClients\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));

            string response = _server.SendData(cmds);
            var clients = JsonConvert.DeserializeObject<ClientNameRoot>(response);
            return clients;
        }
    }
}
