using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public interface IClientBuilder
    {
        void SetSocket(Socket socket);
        void SetId(int Id);
        void SetCommandHistory(List<string> list);
        void SetStatus(int status);
        void SetTerminated(bool terminated);
        Client GetClient();
    }
}
