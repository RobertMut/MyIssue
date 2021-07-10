using System.Net;
using System.Net.Sockets;

namespace MyIssue.Core.Entities
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
