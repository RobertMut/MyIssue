using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Net net;
            Net conn = new Connection();
            IdentifyClient.Clients = 0;
            Parameters.BufferSize = 1024;
            Parameters.Timeout = 10000;
            try
            {
                conn.Listen("127.0.0.1", 49153);
                Console.ReadKey();
                //communication.ip = "127.0.0.1";
                //communication.port = 49153;

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
