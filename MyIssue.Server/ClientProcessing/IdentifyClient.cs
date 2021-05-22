using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public class IdentifyClient
    {
        public Client Identify(IClientBuilder _builder, Socket s, int Id, List<string> c, int status, bool t)
        {
            _builder.SetSocket(s);
            _builder.SetId(Id);
            _builder.SetCommandHistory(c);
            _builder.SetStatus(status);
            _builder.SetTerminated(t);
            return _builder.GetClient();
        }

    }
}
