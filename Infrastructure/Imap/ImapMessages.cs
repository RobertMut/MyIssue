using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyIssue.Core.Commands;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Core.Interfaces;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Files;
using MyIssue.Infrastructure.Model;
using MyIssue.Infrastructure.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyIssue.Infrastructure.Imap
{
    public class ImapMessages : IImapParse
    {
        private IImapConnect _iconn;
        private ImapClient idleClient;
        private readonly string _login;
        private readonly string _pass;
        private ImapClient getClient;
        private CancellationToken token;
        private CancellationTokenSource cancelToken;
        private CancellationTokenSource doneSrc;
        private bool freshRun = true;
        private IServerConnector _connector;

        public ImapMessages(ImapClient idleClient, string apiAddress, int port, string login, string pass)
        {
            _iconn = new ImapConnect();
            this.idleClient = idleClient;
            _connector = new ServerConnector(apiAddress, port);
            _login = login;
            _pass = pass;
            cancelToken = new CancellationTokenSource();
        }

        public async Task ImapListenNewMessagesAsync(CancellationToken ct)
        {
            token = CancellationTokenSource.CreateLinkedTokenSource(ct).Token;
            cancelToken = new CancellationTokenSource();
            await idleClient.Inbox.OpenAsync(FolderAccess.ReadOnly, token);
            try
            {
                idleClient.Inbox.CountChanged += Inbox_CountChangedAsync;
                Console.WriteLine("IMAP - {0} - Waiting for messages..", DateTime.Now);
                await IdleAsync();
            }
            catch (ServiceNotConnectedException snce)
            {
                SerilogLogger.ServerLogException(snce);
                idleClient.Connect(ImapParameters.Parameters.Address, ImapParameters.Parameters.Port, ImapParameters.Parameters.SocketOptions);
                idleClient.Authenticate(ImapParameters.Parameters.Login, ImapParameters.Parameters.Password);
            }
        }
        private async Task IdleAsync()
        {
            if (freshRun) Inbox_CountChangedAsync(this, EventArgs.Empty);
            while (!cancelToken.Token.IsCancellationRequested)
            {
                try
                {
                    await WaitForMessages();
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
        private async Task WaitForMessages()
        {
            while (true)
            {
                cancelToken.Token.ThrowIfCancellationRequested();
                try
                {

                    if (idleClient.Capabilities.HasFlag(ImapCapabilities.Idle))
                    {
                        doneSrc = new CancellationTokenSource(new TimeSpan(0, 9, 0));
                        try
                        {
                            await idleClient.IdleAsync(doneSrc.Token, cancelToken.Token);
                        }
                        finally
                        {
                            doneSrc.Dispose();
                            doneSrc = null;
                        }
                    }
                    else
                    {
                        await System.Threading.Tasks.Task.Delay(new TimeSpan(0, 1, 0), cancelToken.Token);
                        await idleClient.NoOpAsync(cancelToken.Token);
                    }
                    break;
                }
                catch (ImapProtocolException iex)
                {
                    SerilogLogger.ServerLogException(iex);
                }
                catch (IOException ioe)
                {
                    await _iconn.ReconnectAsync(idleClient);
                    SerilogLogger.ServerLogException(ioe);
                }
                catch (OperationCanceledException)
                {
                    doneSrc?.Cancel();
                    break;
                }
            }
        }

        public async void Inbox_CountChangedAsync(object sender, EventArgs e)
        {
            Console.WriteLine("IMAP - {0} - Message counter changed!", DateTime.Now);
            try
            {
                using (getClient = new ImapClient())
                {
                    if (freshRun) freshRun = false;
                    _iconn.ConnectToImap(getClient, token);
                    await getClient.Inbox.OpenAsync(FolderAccess.ReadWrite, token);
                    getClient.Inbox.SearchAsync(SearchQuery.NotSeen.And(
                    SearchQuery.SubjectContains("[Issue]"))
                    ).Result.ToList().ForEach(s =>
                    {
                        try
                        {
                            Console.WriteLine("IMAP - {0} - Writing found messages to database..", DateTime.Now);
                            WriteToDatabase(getClient.Inbox.GetMessage(s));
                            getClient.Inbox.AddFlags(s, MessageFlags.Seen, true);
                        }
                        catch (Exception ex)
                        {
                            SerilogLogger.ServerLogException(ex);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                SerilogLogger.ServerLogException(ex);
                CancellationTokenSource.CreateLinkedTokenSource(token).Cancel();

            }
            finally
            {
                doneSrc = null;
                cancelToken = new CancellationTokenSource();
            }

        }

        public void WriteToDatabase(MimeMessage m)
        {

            string[] email = StringStatic.SplitBrackets(m.Subject, '[', ']').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            string desc =
                $"{email[2]} {email[3]}\n{(string.IsNullOrWhiteSpace(m.TextBody) ? "No description.." : m.TextBody)}";
            IEnumerable<byte[]> cmds = new List<byte[]>()
                        .Concat(User.Login(_login, _pass))
                        .Append(StringStatic.ByteMessage("CreateTask\r\n<EOF>\r\n"))
                        .Append(StringStatic.ByteMessage($"{email[4]}\r\n<NEXT>\r\n{desc}\r\n<NEXT>\r\n{email[1]}\r\n<NEXT>\r\n" +
                                                         $"{"null"}\r\n<NEXT>\r\n{"null"}\r\n<NEXT>\r\nNormal\r\n<NEXT>\r\n" +
                                                         $"\r\n<NEXT>\r\n\r\n<NEXT>\r\n" +
                                                         $"{"null"}\r\n<EOF>\r\n"))
                        .Append(StringStatic.ByteMessage("Logout\r\n<EOF>\r\n"));

            string response = _connector.SendData(cmds);

            Console.WriteLine($"IMAP - {DateTime.Now} - {response}");
        }
    }

}

//0:[Issue]1:[Client]2:[Name]3:[Surname]4:[Title]