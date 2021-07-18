using MyIssue.Core.Constants;
using MyIssue.Core.Entities;
using System.Data.SqlClient;

namespace MyIssue.Infrastructure.Database.SqlCommands
{
    public class Login : SqlCmd
    {

        public override SqlCommand SqlCommand(SqlCommandInput input)
        {
            string selectQuery = string.Format(QueryString.selectLogin, input.Table, input.Command[0], input.Command[1]);
            return new SqlCommand(selectQuery);
        }
    }
}