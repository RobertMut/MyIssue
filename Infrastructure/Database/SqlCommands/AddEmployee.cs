using MyIssue.Core.Constants;
using MyIssue.Core.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MyIssue.Infrastructure.Database.SqlCommands
{
    public class AddEmployee : SqlCmd
    {
        public override SqlCommand SqlCommand(SqlCommandInput input)
        {
            string employee = string.Format(QueryString.insertEmployee, input.Table);
            using (SqlCommand cmd = new SqlCommand(employee))
            {
                int? position = _t.NullableInt(input.Command[4]);
                cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar, 10).Value = input.Command[0];
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar, 70).Value = input.Command[1];
                cmd.Parameters.Add("@SURNAME", SqlDbType.VarChar, 70).Value = input.Command[2];
                cmd.Parameters.Add("@NO", SqlDbType.Decimal).Value = input.Command[3];
                cmd.Parameters.Add("@POSITION", SqlDbType.Decimal).Value = position == null ? (object)DBNull.Value : position;
                return cmd;
            }
        }
    }
}