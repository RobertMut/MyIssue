using System;
using System.Net.Http;
using System.Threading;
using MyIssue.Infrastructure.Model;
using MyIssue.Server.Model;

namespace MyIssue.Server.Commands
{
    public abstract class Command
    {
        protected HttpClient httpclient;
        public Command()
        {
            httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri(Parameters.Api);
        }

        public abstract void Invoke(Model.Client client, CancellationToken ct);
    }

}
