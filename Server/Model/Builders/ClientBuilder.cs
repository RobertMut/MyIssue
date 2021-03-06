using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace MyIssue.Server.Model.Builders
{
    public class ClientBuilder : IClientBuilder
    {
        protected Client client;

        private ClientBuilder()
        {
            client = new Client();
        }
        public IClientBuilder SetSocket(Socket socket)
        {
            client.ConnectedSock = socket;
            return this;
        }
        public IClientBuilder SetId(int id)
        {
            client.Login = Convert.ToString(id);
            return this;
        }
        public IClientBuilder SetCommandHistory(List<string> list)
        {
            client.CommandHistory = list;
            return this;
        }
        public IClientBuilder SetStatus(int status)
        {
            client.Status = status;
            return this;
        }
        public IClientBuilder SetTerminated(bool terminated)
        {
            client.Terminated = terminated;
            return this;
        }


        public IClientBuilder SetPassword(string password)
        {
            client.Password = password;
            return this;
        }
        public Client Build() => client;
        public static ClientBuilder Create() => new ClientBuilder();
    }
}
