using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Linq;
using System.Threading;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyIssue.Core.Interfaces;
using MyIssue.Core.Model.Return;
using MyIssue.Core.String;
using MyIssue.Infrastructure.Files;
using MyIssue.Infrastructure.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyIssue.Infrastructure.Imap
{
    public class ImapMessages : IImapParse 
    {
        private IImapConnect _iconn;
        private ImapClient idleClient;
        private readonly string _apiAddress;
        private readonly string _login;
        private readonly string _pass;
        private ImapClient getClient;
        private CancellationToken token;
        private CancellationTokenSource cancelToken;
        private CancellationTokenSource doneSrc;
        private bool freshRun = true;


        public ImapMessages(ImapClient idleClient, string apiAddress, string login, string pass)
        {
            _iconn = new ImapConnect();
            this.idleClient = idleClient;
            _apiAddress = apiAddress;
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
            string encoded = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{_login}:{_pass}")
            );
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiAddress);
                using (var request = new HttpRequestMessage(HttpMethod.Post,
                    client.BaseAddress + $"api/Tasks/imap"))
                {
                    var json = JsonConvert.SerializeObject(new TaskReturn
                    {
                        TaskTitle = email[4],
                        TaskDescription = desc,
                        TaskClient = email[1],
                        TaskAssignment = null,
                        TaskOwner = null,
                        TaskType = "Normal",
                        CreatedByMail = m.GetHashCode().ToString()
                    });
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                    request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                    request.Headers.Connection.Add("keep-alive");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", $"Basic {encoded}");
                    var response = client.SendAsync(request).Result;

                    Console.WriteLine($"IMAP - {DateTime.Now} - {response.StatusCode}");
                }
            }

        }

    }
}
//0:[Issue]1:[Client]2:[Name]3:[Surname]4:[Title]