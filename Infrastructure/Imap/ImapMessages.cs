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
using MyIssue.Core.String;
using MyIssue.Infrastructure.Database;
using MyIssue.Infrastructure.Files;
using MyIssue.Infrastructure.Model;

namespace MyIssue.Infrastructure.Imap
{
    public class ImapMessages : IImapParse 
    {
        private IImapConnect _iconn;
        private UnitOfWork unit;

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
            unit = new UnitOfWork(new Database.Models.MyIssueContext(DBParameters.ConnectionString.ToString()));
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
            string title = email[4];
            string desc = string.Format("{0} {1}\n{2}", email[2], email[3], string.IsNullOrWhiteSpace(m.TextBody) ? "No description.." : m.TextBody);
            string client = email[1];
            decimal clientId = unit.ClientRepository.Get(c => c.ClientName == client).FirstOrDefault().ClientId;


             unit.TaskRepository.Add(new Database.Models.Task
            {
                TaskTitle = title,
                TaskDesc = desc,
                TaskCreation = m.Date.DateTime,
                TaskClient = clientId,
                TaskType = 1,
                MailId = m.GetHashCode().ToString()
            });
            unit.Complete();
            Console.WriteLine("IMAP - {0} - Data was written to database", DateTime.Now);
        }

    }
}
//0:[Issue]1:[Client]2:[Name]3:[Surname]4:[Title]