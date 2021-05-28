using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public static class LogUser
    {
        public static void TypedCommand(string method, string message, Client c)
        {
            Console.WriteLine("{0} - {1} - {2} - {3} {4}", c.ConnectedSock.RemoteEndPoint, c.Id, DateTime.Now, message, method);
        }
    }
}
