using System;
using System.Net.Http;
using System.Threading;
using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Database;
using MyIssue.Infrastructure.Database.Models;
using MyIssue.Infrastructure.Model;

namespace MyIssue.Server.Commands
{
    public abstract class Command
    {
        protected HttpClient httpclient;
        public Command()
        {
            httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("https://localhost:5003");
        }

        public abstract void Invoke(Model.Client client, CancellationToken ct);
    }

}
