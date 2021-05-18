using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server.Comm
{
    public abstract class Net
    {


        public abstract Task Listen(string ipAddres, int port, int bufferSize = 1024);

    }
}
