using System.Threading;
using MyIssue.Core.Entities;
using MyIssue.Infrastructure.Database;
using MyIssue.Infrastructure.Database.Models;
using Client = MyIssue.Core.Entities.Client;

namespace MyIssue.Server.Commands
{
    public abstract class Command
    {
        protected UnitOfWork unit;
        public Command()
        {
            unit = new UnitOfWork(new MyIssueContext(DBParameters.ConnectionString.ToString()));
        }

        public abstract void Invoke(Client client, CancellationToken ct);
    }

}
