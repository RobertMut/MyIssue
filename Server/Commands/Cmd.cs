using System.Threading;
using System.Data.SqlClient;
using MyIssue.Core.Interfaces;
using MyIssue.Core.Entities;
using MyIssue.Infrastructure.Database;

namespace MyIssue.Server.Commands
{
    public abstract class Cmd
    {
        protected readonly IDBConnector _connector;
        protected readonly SqlConnectionStringBuilder cString;
        protected readonly ISqlCommandParser _sqlCommandParser;
        public Cmd()
        {
            cString = DBParameters.ConnectionString;
            _connector = new DBConnector();
            _sqlCommandParser = new SqlCommandParser();

        }

        public abstract void Command(Client client, CancellationToken ct);
    }

}
