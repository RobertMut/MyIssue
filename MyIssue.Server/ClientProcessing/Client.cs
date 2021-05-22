using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public class Client
    {
        public Socket ConnectedSock { get; set; }
        public int Id { get; set; }
        public List<string> CommandHistory { get; set; }
        public int Status { get; set; }
        public bool Terminated { get; set; }


    }
}
