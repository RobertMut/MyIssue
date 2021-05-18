using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Database
{
    public class Queries
    {
        public SqlCommand InsertNewTask(string[] values, string table) {
            string insertQuery = "INSERT INTO "+table+"(taskTitle, taskDesc, taskCreation, taskClient, taskType)VALUES(@TITLE, @DESC, @DATE, @CLIENT, @TYPE)";
            Tools t = new Tools();
            using (SqlCommand cmd = new SqlCommand(insertQuery))
            {
                DateTime.TryParse(values[2], out DateTime date);
                int? client = t.NullableInt(values[3]);
                cmd.Parameters.Add("@TITLE", SqlDbType.VarChar,100).Value = values[0];
                cmd.Parameters.Add("@DESC", SqlDbType.VarChar).Value = values[1];
                cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(values[2]);
                cmd.Parameters.Add("@CLIENT", SqlDbType.Decimal).Value = client == null ? (object)DBNull.Value : t.NullableInt(values[3]);
                cmd.Parameters.Add("@TYPE", SqlDbType.Decimal).Value = Convert.ToDecimal(values[4]);
                return cmd;
            }

        }
    }
}
