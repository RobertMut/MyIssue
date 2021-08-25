using System;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Server.Net;
using MyIssue.Core.Interfaces;
using MyIssue.Core.Exceptions;
using MyIssue.Infrastructure.Files;
using System.Reflection;
using System.Data.SqlClient;
using MyIssue.Infrastructure.Database;
using MyIssue.Infrastructure.Imap;
using DBParameters = MyIssue.Infrastructure.Model.DBParameters;

namespace MyIssue.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IArchiveFile _archiveFile = new ArchiveFile();
            IImapConnect _imap = new ImapConnect();
            LogWriter.Init(_archiveFile);
            INetwork _net;
            


            CancellationToken ct = new CancellationToken();
            try
            {
                Console.WriteLine("START - {0} - Opening configuration file..", DateTime.Now);
                var config = OpenConfiguration.OpenConfig("configuration.xml");
                Bootstrapper.InitializeParameters(config);
                Console.WriteLine("DB - {0} - Connecting to database..", DateTime.Now);
                IDatabaseBootstrapper _dbBootstrapper = new DatabaseBootstrapper(DBParameters.ConnectionString.ConnectionString);
                _dbBootstrapper.Configure();
                Console.WriteLine("DB - {0} - OK", DateTime.Now);

                string listen = ConfigValue.GetValue<string>("listenAddress", config);
                int port = ConfigValue.GetValue<int>("port", config);
                _net = new NetListener(listen, port);

                if (ConfigValue.GetValue<string>("enabled", config).Equals("true")) Task.Run(async () => _net.Listen());
                if (ConfigValue.GetValue<string>("i_enabled", config).Equals("true")) Task.Run(async () => _imap.RunImap(ct));
                Console.ReadKey();

            }
            catch (ConfigurationNotFoundException confex)
            {
                SerilogLogger.ServerLogException(confex);
                string emptyConfig = LoadFile.Load(Assembly.Load("Infrastructure").GetManifestResourceStream("MyIssue.Infrastructure.Resources.configurationServer.xml"));
                WriteConfiguration.WriteEmptyConfig("configuration.xml", emptyConfig);
                Console.WriteLine("IO - {0} - Please fill configuration file!", DateTime.Now);
                Console.ReadKey();
            }
            catch (InvalidOperationException ioe)
            {
                SerilogLogger.ServerLogException(ioe);
                Console.ReadKey();
            } catch (NullReferenceException nre)
            {
                SerilogLogger.ServerLogException(nre);
                Console.WriteLine("IO - {0} - Detected empty value. Please fill configuration file", DateTime.Now);
                Console.ReadKey();
            } catch (SqlException sql)
            {
                SerilogLogger.ServerLogException(sql);
                Console.WriteLine("DB - {0} - SqlException. Check database user permissions", DateTime.Now);
                Console.ReadKey();
            }
        }
    }
}