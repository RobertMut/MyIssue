using System.Collections.Generic;
using System.Net.Sockets;
using MyIssue.Core.Entities;

namespace MyIssue.Core.Interfaces
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
