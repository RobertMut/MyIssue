using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    class Program
    {
        public static Database.DBParameters dbParameters;
        static void Main(string[] args)
        {
            //Conn.Net net;
            Comm.IListen listen = new Comm.Listen();
            ClientCounter.Clients = 0;
            Comm.Parameters.BufferSize = 1024;
            Comm.Parameters.Timeout = 10000;
            dbParameters = new Database.DBParameters()
            {
                DBAddress = "DESKTOP-F8Q65V7",
                Username = "server",
                Password = "1234",
                Database = "MyIssueDB",
                TaskTable = "dbo.tasks",
                UsersTable = "dbo.users",
                EmployeesTable = "dbo.employees"
            };
            
            try
            {
                listen.Listener("127.0.0.1", 49153);
                Console.ReadKey();
                //communication.ip = "127.0.0.1";
                //communication.port = 49153;

            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }

        }
    }
}
