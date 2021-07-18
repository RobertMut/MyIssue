using MyIssue.Core.Constants;
using MyIssue.Core.Entities;
using System.Data.SqlClient;

namespace MyIssue.Infrastructure.Database.SqlCommands
{
    public class BanUser : SqlCmd
    {

        public override SqlCommand SqlCommand(SqlCommandInput input) => new SqlCommand(
                string.Format(QueryString.banUser, input.Table, input.Command)
                );
    }
}