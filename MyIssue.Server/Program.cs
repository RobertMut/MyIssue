using System;
using System.Data.SqlClient;
using MyIssue.Server.Net;
using MyIssue.Server.Database;
using MyIssue.Server.Imap;
using MyIssue.Server.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace MyIssue.Server
{
    class Program
    {

        static void Main(string[] args)
        {

            IArchiveFile _archiveFile = new ArchiveFile();
            INetwork _net = new Network();
            IImapConnect _imap = new ImapConnect();
            IWriteConfig _write = new WriteConfiguration();
            IReadConfig _read = new OpenConfiguration();
            IConfigValue _c;

            LogWriter.Init(_archiveFile);
            ClientCounter.Clients = 0;
            try
            {
                CancellationToken ct = new CancellationToken();
                bool newConfig = _write.WriteEmptyConfig();
                if (newConfig) throw new ConfigurationNotFoundException("Configuration file does not exist");
                if (!newConfig)
                {
                    var config = _read.OpenConfig();
                    _c = new ConfigValue(config);
                    string listen = _c.GetValue("listenAddress");
                    int port = Convert.ToInt32(_c.GetValue("port"));
                    Parameters.BufferSize = Convert.ToInt32(_c.GetValue("bufferSize"));
                    Parameters.Timeout = Convert.ToInt32(_c.GetValue("timeout"));

                    ImapParameters.Parameters = ImapParametersBuilder
                        .Create()
                            .SetAddress(_c.GetValue("i_address"))
                            .SetPort(Convert.ToInt32(_c.GetValue("i_port")))
                            .SetSocketOptions(
                                (MailKit.Security.SecureSocketOptions)Enum.Parse(
                                    typeof(MailKit.Security.SecureSocketOptions), _c.GetValue("i_connectionOptions")))
                            .SetLogin(_c.GetValue("i_login"))
                            .SetPassword(_c.GetValue("i_password"))
                        .Build();

                    DBParameters.Parameters = DBParametersBuilder
                        .Create()
                            .SetDBAddress(_c.GetValue("d_address"))
                            .SetDatabase(_c.GetValue("d_database"))
                            .SetUsername(_c.GetValue("d_username"))
                            .SetPassword(_c.GetValue("d_password"))
                            .SetEmployeesTable(_c.GetValue("d_employeesTable"))
                            .SetUsersTable(_c.GetValue("d_usersTable"))
                            .SetTaskTable(_c.GetValue("d_taskTable"))
                            .SetClientsTable(_c.GetValue("d_clientsTable"))
                        .Build();

                    DBParameters.ConnectionString = new SqlConnectionStringBuilder()
                    {
                        DataSource = DBParameters.Parameters.DBAddress,
                        UserID = DBParameters.Parameters.Username,
                        Password = DBParameters.Parameters.Password,
                        InitialCatalog = DBParameters.Parameters.Database
                    };

                    if (_c.GetValue("enabled").Equals("true")) Task.Run(async () => _net.Listener(listen, port));
                    if (_c.GetValue("i_enabled").Equals("true")) Task.Run(async () => _imap.RunImap(ct));
                    Console.ReadKey();
                }

            }
            catch (ConfigurationNotFoundException confex)
            {
                ExceptionHandler.HandleMyException(confex);
                Console.WriteLine("IO - {0} - Please fill configuration file!", DateTime.Now);
                Console.ReadKey();
            }
            catch (InvalidOperationException ioe)
            {
                ExceptionHandler.HandleMyException(ioe);
                Console.ReadKey();
            } catch (NullReferenceException nre)
            {
                ExceptionHandler.HandleMyException(nre);
                Console.WriteLine("IO - {0} - Detected empty value. Please fill configuration file", DateTime.Now);
                Console.ReadKey();
            }
        }
    }
}