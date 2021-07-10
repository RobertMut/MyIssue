using System;
using System.Data.SqlClient;
using MyIssue.Server.Net;
using MyIssue.Server.Database;
using MyIssue.Server.Imap;
using MyIssue.Core.IO;
using MyIssue.Server.IO;
using MyIssue.Core.Interfaces;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

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
            LogWriter.Init(_archiveFile);
            ClientCounter.Clients = 0;
            try
            {
                CancellationToken ct = new CancellationToken();
                bool newConfig = _write.WriteEmptyConfig("configuration.xml",Config.emptyConfig);
                if (newConfig) throw new ConfigurationNotFoundException("Configuration file does not exist");
                if (!newConfig)
                {
                    var config = _read.OpenConfig("configuration.xml");
                    string listen = ConfigValue.GetValue("listenAddress", config);
                    int port = Convert.ToInt32(ConfigValue.GetValue("port", config));
                    Parameters.BufferSize = Convert.ToInt32(ConfigValue.GetValue("bufferSize", config));
                    Parameters.Timeout = Convert.ToInt32(ConfigValue.GetValue("timeout", config));

                    ImapParameters.Parameters = ImapParametersBuilder
                        .Create()
                            .SetAddress(ConfigValue.GetValue("i_address", config))
                            .SetPort(Convert.ToInt32(ConfigValue.GetValue("i_port", config)))
                            .SetSocketOptions(
                                (MailKit.Security.SecureSocketOptions)Enum.Parse(
                                    typeof(MailKit.Security.SecureSocketOptions), ConfigValue.GetValue("i_connectionOptions", config)))
                            .SetLogin(ConfigValue.GetValue("i_login", config))
                            .SetPassword(ConfigValue.GetValue("i_password", config))
                        .Build();

                    DBParameters.Parameters = DBParametersBuilder
                        .Create()
                            .SetDBAddress(ConfigValue.GetValue("d_address", config))
                            .SetDatabase(ConfigValue.GetValue("d_database", config))
                            .SetUsername(ConfigValue.GetValue("d_username", config))
                            .SetPassword(ConfigValue.GetValue("d_password", config))
                            .SetEmployeesTable(ConfigValue.GetValue("d_employeesTable", config))
                            .SetUsersTable(ConfigValue.GetValue("d_usersTable", config))
                            .SetTaskTable(ConfigValue.GetValue("d_taskTable", config))
                            .SetClientsTable(ConfigValue.GetValue("d_clientsTable", config))
                        .Build();

                    DBParameters.ConnectionString = new SqlConnectionStringBuilder()
                    {
                        DataSource = DBParameters.Parameters.DBAddress,
                        UserID = DBParameters.Parameters.Username,
                        Password = DBParameters.Parameters.Password,
                        InitialCatalog = DBParameters.Parameters.Database
                    };

                    if (ConfigValue.GetValue("enabled", config).Equals("true")) Task.Run(async () => _net.Listener(listen, port));
                    if (ConfigValue.GetValue("i_enabled", config).Equals("true")) Task.Run(async () => _imap.RunImap(ct));
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