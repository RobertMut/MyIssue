using System;
using System.Data.SqlClient;
using MyIssue.Server.Net;
using MyIssue.Server.Database;
using MyIssue.Server.Imap;
using System.Threading;

namespace MyIssue.Server
{
    class Program
    {

        static void Main(string[] args)
        {

            IArchiveFile _archiveFile = new ArchiveFile();
            INetwork _net = new Network();
            IImapConnect _imap = new ImapConnect();
            LogWriter.Init(_archiveFile);
            

            ClientCounter.Clients = 0;
            Parameters.BufferSize = 1024;
            Parameters.Timeout = 10000;
            ImapParameters.Parameters = ImapParametersBuilder
                .Create()
                    .SetAddress("127.0.0.1")
                    .SetPort(143)
                    .SetSocketOptions(MailKit.Security.SecureSocketOptions.Auto)
                    .SetLogin("root")
                    .SetPassword("1234")
                .Build();
            DBParameters.Parameters = DBParametersBuilder
                .Create()
                    .SetDBAddress("DESKTOP-F8Q65V7")
                    .SetDatabase("MyIssueDB")
                    .SetUsername("server")
                    .SetPassword("1234")
                    .SetEmployeesTable("dbo.EMPLOYEES")
                    .SetUsersTable("dbo.USERS")
                    .SetTaskTable("dbo.TASKS")
                    .SetClientsTable("dbo.CLIENTS")
                .Build();
            DBParameters.ConnectionString = new SqlConnectionStringBuilder()
            {
                DataSource = DBParameters.Parameters.DBAddress,
                UserID = DBParameters.Parameters.Username,
                Password = DBParameters.Parameters.Password,
                InitialCatalog = DBParameters.Parameters.Database
            };
            try
            {
                CancellationToken ct = new CancellationToken();
                _imap.RunImap(ct);
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
