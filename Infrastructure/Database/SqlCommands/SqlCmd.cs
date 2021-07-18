using System.Data.SqlClient;
using MyIssue.Core.String;
using MyIssue.Core.Interfaces;
using MyIssue.Core.Entities;

namespace MyIssue.Infrastructure.Database.SqlCommands
{
    public abstract class SqlCmd
    {
        protected IStringTools _t;
        public SqlCmd()
        {
            _t = new StringTools();
        }
        public abstract SqlCommand SqlCommand(SqlCommandInput input);
    }

}
