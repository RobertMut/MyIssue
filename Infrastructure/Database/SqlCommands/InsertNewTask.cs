using MyIssue.Core.Constants;
using MyIssue.Core.Entities;
using System;
using System.Data.SqlClient;

namespace MyIssue.Infrastructure.Database.SqlCommands
{
    public class InsertNewTask : SqlCmd
    {
        public override SqlCommand SqlCommand(SqlCommandInput input)
        {
            string insertQuery = string.Format(QueryString.insertTask, input.Table);
            using (SqlCommand cmd = new SqlCommand(insertQuery))
            {
                DateTime.TryParse(input.Command[2], out DateTime date);
                int? client = _t.NullableInt(input.Command[3]);
                cmd.Parameters.AddWithValue("@TITLE", input.Command[0]);
                cmd.Parameters.AddWithValue("@DESC", input.Command[1]);
                cmd.Parameters.AddWithValue("@DATE", Convert.ToDateTime(input.Command[2]));
                cmd.Parameters.AddWithValue("@CLIENT", input.Command[3]);
                cmd.Parameters.AddWithValue("@TYPE", Convert.ToInt32(input.Command[4]));
                if (input.Command.Length.Equals(6)) cmd.Parameters.AddWithValue("@MAILID", input.Command[5]);
                else cmd.Parameters.AddWithValue("@MAILID", DBNull.Value);
                return cmd;
            }

        }
    }
}