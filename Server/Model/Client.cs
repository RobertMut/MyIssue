using System.Collections.Generic;
using System.Net.Sockets;

namespace MyIssue.Server.Model
{
    public class Client
    {
        public Socket ConnectedSock { get; set; }
        public string Login { get; set; }
        public List<string> CommandHistory { get; set; }
        public int Status { get; set; }
        public bool Terminated { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
