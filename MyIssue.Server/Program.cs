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
            IArchiveFile _archiveFile = new ArchiveFile();
            LogWriter.Init(_archiveFile);
            Database.DBParameterCoordinator dBcoor = new Database.DBParameterCoordinator();
            Net.INetwork _net = new Net.Network();
            Database.IDBParametersBuilder dBParametersBuilder = new Database.DBParametersBuilder();

            ClientCounter.Clients = 0;
            Net.Parameters.BufferSize = 1024;
            Net.Parameters.Timeout = 10000;
            Database.DBParameters.Parameters=dBcoor.Parameters(
                dBParametersBuilder, "MyIssueDB", "DESKTOP-F8Q65V7", "server", "1234", "dbo.TASKS", "dbo.USERS", "dbo.EMPLOYEES");

           


            try
            {
                
                _net.Listener("127.0.0.1", 49153);
                Console.ReadKey();
                //communication.ip = "127.0.0.1";
                //communication.port = 49153;

            } catch (InvalidOperationException ioe)
            {
                ExceptionHandler.HandleMyException(ioe);
                Console.ReadKey();
            }

        }
    }
}
