﻿using System.Collections.Generic;
using System.Net.Sockets;

namespace MyIssue.Server.Model
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