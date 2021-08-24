using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using MailKit.Security;
using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Entities;
using MyIssue.Infrastructure.Files;

namespace MyIssue.Infrastructure.Imap
{
    public class ImapConnect : IImapConnect
    {
        CancellationToken token;
        ImapClient idleClient;
        IImapParse _parse;
        int tries = 0;

        public async Task RunImap(CancellationToken ct)
        {
            token = ct;
            using (idleClient = new ImapClient())
            {
                _parse = new ImapMessages(idleClient);
                Console.WriteLine("IMAP - {0} - Connecting to server...", DateTime.Now);
                ConnectToImap(idleClient, token);
                var task = _parse.ImapListenNewMessagesAsync(token);
                task.GetAwaiter().GetResult();
            }
        }
        public async Task ConnectToImap(ImapClient c, CancellationToken ct)
        {
            bool tryAgain = true;
            int i = 0;
            while (tryAgain)
            {
                try
                {
                    c.Connect(ImapParameters.Parameters.Address, ImapParameters.Parameters.Port, ImapParameters.Parameters.SocketOptions);
                    c.Authenticate(ImapParameters.Parameters.Login, ImapParameters.Parameters.Password, ct);
                    tryAgain = false;
                    Console.WriteLine("IMAP - {0} - Connected", DateTime.Now);
                }
                catch (SocketException se)
                {
                    i++;
                    Console.WriteLine("IMAP - {0} - Exception occured while connecting to Imap", DateTime.Now);
                    SerilogLogger.ServerLogException(se);
                    if (i.Equals(3))
                    {
                        tryAgain = false;
                        Console.WriteLine("IMAP - {0} - Failed connect to server after 3 tries. Skipping..", DateTime.Now);
                    }
                    
                }
                catch (IOException ioe)
                {
                    SerilogLogger.ServerLogException(ioe);
                    tryAgain = false;
                }
                catch (AuthenticationException auth)
                {
                    SerilogLogger.ServerLogException(auth);
                    Console.WriteLine("IMAP - {0} - Exception occured while authenticating..", DateTime.Now);
                    CancellationTokenSource.CreateLinkedTokenSource(ct).Cancel();
                    tryAgain = false;
                }
            }



        }
        public async Task ReconnectAsync(ImapClient c)
        {
            try
            {
                c.Connect(ImapParameters.Parameters.Address, ImapParameters.Parameters.Port, ImapParameters.Parameters.SocketOptions);
                c.Authenticate(ImapParameters.Parameters.Login, ImapParameters.Parameters.Password);
            }
            catch (SocketException e)
            {
                SerilogLogger.ServerLogException(e);
                tries++;
                Console.WriteLine("IMAP - {0} - Trying to reconnect. Try {1} of {2}", DateTime.Now, tries, 11);
                if (tries < 11) await ReconnectAsync(c);
                else CancellationTokenSource.CreateLinkedTokenSource(token).Cancel();
            }
            catch (AuthenticationException ax)
            {
                SerilogLogger.ServerLogException(ax);
                CancellationTokenSource.CreateLinkedTokenSource(token).Cancel();
            }

        }

    }
}
