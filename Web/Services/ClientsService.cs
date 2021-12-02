using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Model;
using Newtonsoft.Json;

namespace MyIssue.Web.Services
{
    public interface IClientsService
    {
        Task<string> GetClient(TokenAuth model);
        Task<string> PostClient(ClientReturn client, TokenAuth model);
    }

    public class ClientsService : IClientsService
    {
        private readonly IServerConnector _server;

        public ClientsService(IServerConnector server)
        {
            _server = server;
        }

        public async Task<string> GetClient(TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(Core.Commands.User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("GetClients\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));

            return _server.SendData(cmds);
        }

        public async Task<string> PostClient(ClientReturn client, TokenAuth model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(Core.Commands.User.TokenLogin(model.Login, model.Token))
                .Append(StringStatic.ByteMessage("PostClient\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage(
                    $"{client.Name}\r\n<NEXT>\r\n{client.Country}\r\n<NEXT>\r\n{client.No}" +
                    $"\r\n<NEXT>\r\n{client.Street}\r\n<NEXT>\r\n{client.StreetNo}" +
                    $"\r\n<NEXT>\r\n{client.FlatNo}\r\n<NEXT>\r\n{client.Description}\r\n<EOF>\r\n"))
                .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));
            string response = _server.SendData(cmds);
            return response;
        }
    }
}
