using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public class ClientBuilder : IClientBuilder
    {
        private readonly Client _client = new Client();
        public void SetSocket(Socket socket)
        {
            _client.ConnectedSock = socket;
        }
        public void SetId(int Id)
        {
            _client.Id = Id;
        }
        public void SetCommandHistory(List<string> list)
        {
            _client.CommandHistory = list;
        }
        public void SetStatus(int status)
        {
            _client.Status = status;
        }
        public void SetTerminated(bool terminated)
        {
            _client.Terminated = terminated;
        }
        public Client GetClient()
        {
            return _client;
        }
    }
}
