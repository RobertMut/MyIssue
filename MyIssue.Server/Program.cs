using System;
using System.Data.SqlClient;
namespace MyIssue.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IArchiveFile _archiveFile = new ArchiveFile();
            LogWriter.Init(_archiveFile);
            Net.INetwork _net = new Net.Network();

            ClientCounter.Clients = 0;
            Net.Parameters.BufferSize = 1024;
            Net.Parameters.Timeout = 10000;
            Database.DBParameters.Parameters = Database.DBParametersBuilder
                .Create()
                    .SetDBAddress("DESKTOP-F8Q65V7")
                    .SetDatabase("MyIssueDB")
                    .SetUsername("server")
                    .SetPassword("1234")
                    .SetEmployeesTable("dbo.EMPLOYEES")
                    .SetUsersTable("dbo.USERS")
                    .SetTaskTable("dbo.TASKS")
                .Build();
            Database.DBParameters.ConnectionString = new SqlConnectionStringBuilder()
            {
                DataSource = Database.DBParameters.Parameters.DBAddress,
                UserID = Database.DBParameters.Parameters.Username,
                Password = Database.DBParameters.Parameters.Password,
                InitialCatalog = Database.DBParameters.Parameters.Database
            };
            try
            {

                _net.Listener("127.0.0.1", 49153);
                Console.ReadKey();
                //communication.ip = "127.0.0.1";
                //communication.port = 49153;

            }
            catch (InvalidOperationException ioe)
            {
                ExceptionHandler.HandleMyException(ioe);
                Console.ReadKey();
            }

        }
    }
}
