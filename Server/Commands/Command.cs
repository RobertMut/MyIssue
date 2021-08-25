using System.Threading;
using MyIssue.Infrastructure.Database;
using MyIssue.Infrastructure.Database.Models;
using DBParameters = MyIssue.Infrastructure.Model.DBParameters;

namespace MyIssue.Server.Commands
{
    public abstract class Command
    {
        protected UnitOfWork unit;
        public Command()
        {
            unit = new UnitOfWork(new MyIssueContext(DBParameters.ConnectionString.ToString()));
        }

        public abstract void Invoke(Model.Client client, CancellationToken ct);
    }

}
