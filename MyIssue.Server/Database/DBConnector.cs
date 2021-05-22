using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Database
{
    public class DBConnector
    {
        public void MakeQuery(SqlConnectionStringBuilder builder, SqlCommand command)
        {
            using(SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                using(SqlCommand cmd = command)
                {
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("DONE\r\n");
        }
        public SqlConnectionStringBuilder SqlBuilder(DBParametersTemplate parameters)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = parameters.DBAddress;
            builder.UserID = parameters.Username;
            builder.Password = parameters.Password;
            builder.InitialCatalog = parameters.Database;
            return builder;
        }
    }
}
