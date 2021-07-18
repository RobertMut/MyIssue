using System;
using System.Data;
using System.Data.SqlClient;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;

namespace MyIssue.Infrastructure.Database
{
    public class DBConnector : IDBConnector
    {
        public void MakeWriteQuery(SqlConnectionStringBuilder sqlConnection, SqlCommand command)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnection.ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = command)
                    {
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    Console.WriteLine("DB - {0} - Data was written to database\r\n", DateTime.Now);
                }
                catch (Exception sqle)
                {
                    ExceptionHandler.HandleMyException(sqle);
                }



            }

        }
        public DataTable MakeReadQuery(SqlConnectionStringBuilder sqlConnection, SqlCommand command)
        {
            DataSet data = new DataSet();
            using (SqlConnection conn = new SqlConnection(sqlConnection.ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = command)
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd.CommandText, conn))
                    {
                        adapter.Fill(data, "RESULT");
                    }
                    Console.WriteLine("DB - {0} - Data was read from database\r\n", DateTime.Now);
                }
                catch (SqlException sqle)
                {
                    ExceptionHandler.HandleMyException(sqle);

                }
                return data.Tables["RESULT"];
            }
        }

    }
}
