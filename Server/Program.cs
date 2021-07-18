using System;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Server.Net;
using MyIssue.Core.Interfaces;
using MyIssue.Core.Exceptions;
using MyIssue.Infrastructure.Files;
using MyIssue.Infrastructure.Smtp;
using System.Reflection;

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
                var config = OpenConfiguration.OpenConfig("configuration.xml");
                Initializer.InitializeParameters(config);

                string listen = ConfigValue.GetValue("listenAddress", config);
                int port = Convert.ToInt32(ConfigValue.GetValue("port", config));
                _net = new NetListener(listen, port);
                if (ConfigValue.GetValue("enabled", config).Equals("true")) Task.Run(async () => _net.Listen());
                if (ConfigValue.GetValue("i_enabled", config).Equals("true")) Task.Run(async () => _imap.RunImap(ct));
                Console.ReadKey();

            }
            catch (ConfigurationNotFoundException confex)
            {
                ExceptionHandler.HandleMyException(confex);
                string emptyConfig = LoadFile.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream("Infrastructure.Resources.configurationServer.xml"));
                WriteConfiguration.WriteEmptyConfig("configuration.xml", emptyConfig);
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
            finally
            {

            }
        }
    }
}