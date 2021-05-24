using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Database
{
    public class DBConnector
    {
        public void MakeWriteQuery(SqlConnectionStringBuilder builder, SqlCommand command)
        {
            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = command)
                    {
                        Console.WriteLine(cmd.CommandText);
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    Console.WriteLine("DONE\r\n");
                }
                catch (Exception sqle)
                {
                    ExceptionHandler.HandleMyException(sqle);
                }



            }

        }
        public DataTable MakeReadQuery(SqlConnectionStringBuilder builder, SqlCommand command)
        {
            DataSet data = new DataSet();
            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = command)
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd.CommandText, conn))
                    {
                        adapter.Fill(data, "RESULT");
                    }
                    Console.WriteLine("DONE\r\n");
                }
                catch (SqlException sqle)
                {
                    ExceptionHandler.HandleMyException(sqle);

                }
                return data.Tables["RESULT"];
            }
        }
        public SqlConnectionStringBuilder SqlBuilder(DBParametersTemplate parameters)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = parameters.DBAddress,
                UserID = parameters.Username,
                Password = parameters.Password,
                InitialCatalog = parameters.Database
            };
            return builder;
        }
    }
}
