using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Imap;
using MailKit;
using MailKit.Search;
using MimeKit;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using MailKit.Security;

namespace MyIssue.Server.Imap
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
                Console.WriteLine("IMAP - {0} - Connected", DateTime.Now);
                var task = _parse.ImapListenNewMessagesAsync(token);
                task.GetAwaiter().GetResult();
            }
        }
        public async Task ConnectToImap(ImapClient c, CancellationToken ct)
        {
            try
            {
                c.Connect(ImapParameters.Parameters.Address, ImapParameters.Parameters.Port, ImapParameters.Parameters.SocketOptions);
                c.Authenticate(ImapParameters.Parameters.Login, ImapParameters.Parameters.Password, ct);
            }
            catch (SocketException se)
            {
                ExceptionHandler.HandleMyException(se);

            }
            catch (IOException ioe)
            {
                ExceptionHandler.HandleMyException(ioe);
            }
            catch (AuthenticationException auth)
            {
                ExceptionHandler.HandleMyException(auth);
                CancellationTokenSource.CreateLinkedTokenSource(ct).Cancel();
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
                ExceptionHandler.HandleMyException(e);
                tries++;
                Console.WriteLine("IMAP - {0} - Trying to reconnect. Try {1} of {2}", DateTime.Now, tries, 11);
                if (tries < 11) await ReconnectAsync(c);
                else CancellationTokenSource.CreateLinkedTokenSource(token).Cancel();
            }
            catch (AuthenticationException ax)
            {
                ExceptionHandler.HandleMyException(ax);
                CancellationTokenSource.CreateLinkedTokenSource(token).Cancel();
            }

        }

    }
}
