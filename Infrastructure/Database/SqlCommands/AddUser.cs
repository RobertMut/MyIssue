using MyIssue.Core.Constants;
using MyIssue.Core.Entities;
using System.Data;
using System.Data.SqlClient;

namespace MyIssue.Infrastructure.Database.SqlCommands
{
    public class AddUser : SqlCmd
    {

        public override SqlCommand SqlCommand(SqlCommandInput input)
        {
            string insertQuery = string.Format(QueryString.insertUser, input.Table);
            using (SqlCommand cmd = new SqlCommand(insertQuery))
            {
                int.TryParse(input.Command[2], out var r);
                cmd.Parameters.Add("@USERLOGIN", SqlDbType.VarChar, 10).Value = input.Command[0];
                cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar, 128).Value = input.Command[1];
                cmd.Parameters.Add("@TYPE", SqlDbType.Decimal).Value = r;
                return cmd;
            }
        }
    }
}