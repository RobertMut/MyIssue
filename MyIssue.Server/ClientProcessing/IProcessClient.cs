using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public interface IProcessClient
    {
        Task ConnectedTask(Socket sock, CancellationToken ct);
    }
}
