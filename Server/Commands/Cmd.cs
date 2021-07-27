using System.Threading;
using System.Data.SqlClient;
using MyIssue.Core.Interfaces;
using MyIssue.Core.Entities;
using MyIssue.Infrastructure.Database;
using MyIssue.Core.Entities.Database;

namespace MyIssue.Server.Commands
{
    public abstract class Cmd
    {
        protected readonly SqlConnectionStringBuilder cString;
        protected readonly IUnitOfWork unitOfWork;
        public Cmd()
        {
            cString = DBParameters.ConnectionString;
            unitOfWork = new UnitOfWork(
                new MyIssueDatabase(
                    DBParameters.ConnectionString.ToString()));

        }

        public abstract void Command(Client client, CancellationToken ct);
    }

}
