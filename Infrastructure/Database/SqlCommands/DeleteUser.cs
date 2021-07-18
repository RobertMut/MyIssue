using MyIssue.Core.Constants;
using MyIssue.Core.Entities;
using System.Data.SqlClient;

namespace MyIssue.Infrastructure.Database.SqlCommands
{
    public class DeleteUser : SqlCmd
    {

        public override SqlCommand SqlCommand(SqlCommandInput input)
        {
            return new SqlCommand(
                string.Format(QueryString.deleteUser, input.Table, input.Command[0])
                );
        }
    }
}