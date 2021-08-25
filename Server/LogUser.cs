using System;

namespace MyIssue.Server
{
    public static class LogUser
    {
        public static void TypedCommand(string method, string message, Model.Client c)
        {
            Console.WriteLine("{0} - {1} - {2} - {3} {4}", c.ConnectedSock.RemoteEndPoint, c.Id, DateTime.Now, message, method);
        }
    }
}
