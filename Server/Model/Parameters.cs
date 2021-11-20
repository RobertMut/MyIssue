using System.Net;
using System.Net.Sockets;

namespace MyIssue.Server.Model
{
    public static class Parameters
    {

        public static int BufferSize { get; set; }
        public static byte[] ConnBuffer { get; set; }
        public static int Timeout { get; set; }
        public static IPEndPoint EndPoint { get; set; }
        public static Socket ListenSocket { get; set; }
        public static string Api { get; set; }
        public static string AuthAddress { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
    }
}
