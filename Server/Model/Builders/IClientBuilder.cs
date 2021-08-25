using System.Collections.Generic;
using System.Net.Sockets;

namespace MyIssue.Server.Model.Builders
{
    public interface IClientBuilder
    {
        IClientBuilder SetSocket(Socket socket);
        IClientBuilder SetId(int Id);
        IClientBuilder SetCommandHistory(List<string> list);
        IClientBuilder SetStatus(int status);
        IClientBuilder SetTerminated(bool terminated);
        Client Build();
    }
}
