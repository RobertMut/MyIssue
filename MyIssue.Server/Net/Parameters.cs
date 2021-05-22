using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Net
{
    public static class Parameters
    {

        public static int BufferSize { get; set; }
        public static byte[] ConnBuffer { get; set; }
        public static int Timeout { get; set; }
        public static IPEndPoint EndPoint { get; set; }
        public static Socket ListenSocket { get; set; }
    }
}
