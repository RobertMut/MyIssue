using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Linq;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using MyIssue.Core.Interfaces;
using MyIssue.Core.Entities;
using MyIssue.Core.String;
using MyIssue.Core.Exceptions;
using MyIssue.Infrastructure.Smtp;
using MyIssue.Infrastructure.Database;
using MyIssue.Core.Entities.Builders;

namespace MyIssue.Infrastructure.Imap
{
    public class ImapMessages : IImapParse
    {
        private IStringTools _tools;
        private IDBConnector _conn;
        private IImapConnect _iconn;
        private readonly ISqlCommandParser _sqlCommandParser;

        private ImapClient idleClient;
        private ImapClient getClient;
        private CancellationToken token;
        private CancellationTokenSource cancelToken;
        private CancellationTokenSource doneSrc;
        private bool freshRun = true;


        public ImapMessages(ImapClient idleClient)
        {
            _iconn = new ImapConnect();
            this.idleClient = idleClient;
            cancelToken = new CancellationTokenSource();
            _sqlCommandParser = new SqlCommandParser();
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
                ExceptionHandler.HandleMyException(snce);
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
                        await Task.Delay(new TimeSpan(0, 1, 0), cancelToken.Token);
                        await idleClient.NoOpAsync(cancelToken.Token);
                    }
                    break;
                }
                catch (ImapProtocolException iex)
                {
                    ExceptionHandler.HandleMyException(iex);
                }
                catch (IOException ioe)
                {
                    await _iconn.ReconnectAsync(idleClient);
                    ExceptionHandler.HandleMyException(ioe);
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
                            ExceptionHandler.HandleMyException(ex);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleMyException(ex);
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
            _tools = new StringTools();
            _conn = new DBConnector();

            string[] email = _tools.SplitBrackets(m.Subject, '[', ']').Where(x => !string.IsNullOrEmpty(x)).ToArray();


            var query = _sqlCommandParser.SqlCmdParser("InsertNewTask", SqlCommandInputBuilder
           .Create()
               .SetCommandFromImap(m.Subject, m.TextBody, m.GetHashCode(), m.Date.DateTime)
               .SetTable(DBParameters.Parameters.TaskTable)
           .Build());
            _conn.MakeWriteQuery(DBParameters.ConnectionString, query);
        }

    }
}
//0:[Issue]1:[Client]2:[Name]3:[Surname]4:[Title]