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
            string insertQuery = string.Format("INSERT INTO {0}(taskTitle, taskDesc, taskCreation, taskClient, taskType)VALUES(@TITLE, @DESC, @DATE, @CLIENT, @TYPE)", table);
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
        public SqlCommand AddEmployee(string[] values, string table)
        {
            Tools t = new Tools();
            string employee = string.Format("INSERT INTO {0} VALUES(@LOGIN, @NAME, @SURNAME, @NO, @POSITION)", table);
            using (SqlCommand cmd = new SqlCommand(employee))
            {
                int? position = t.NullableInt(values[4]);
                cmd.Parameters.Add("@LOGIN", SqlDbType.VarChar, 10).Value = values[0];
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar, 70).Value = values[1];
                cmd.Parameters.Add("@SURNAME", SqlDbType.VarChar, 70).Value = values[2];
                cmd.Parameters.Add("@NO", SqlDbType.Decimal).Value = values[3];
                cmd.Parameters.Add("@POSITION", SqlDbType.Decimal).Value = position == null ? (object)DBNull.Value : position;
                return cmd;
            }
        }

        public SqlCommand AddUser(string[] values, string table)
        {
            string insertQuery = string.Format("INSERT INTO {0} VALUES(@USERLOGIN, @PASSWORD, @TYPE)", table);
            using (SqlCommand cmd = new SqlCommand(insertQuery))
            {
                int.TryParse(values[2], out var r);
                cmd.Parameters.Add("@USERLOGIN", SqlDbType.VarChar, 10).Value = values[0];
                cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar, 128).Value = values[1];
                cmd.Parameters.Add("@TYPE", SqlDbType.Decimal).Value = r;
                return cmd;
            }
        }
        public SqlCommand Login(string[] values, string table)
        {
            string selectQuery = string.Format("SELECT * FROM {0} WHERE \'{1}\' = {0}.userLogin AND {0}.password = \'{2}\'",table,values[0],values[1]);
            using (SqlCommand cmd = new SqlCommand(selectQuery))
            {/*
                cmd.Parameters.AddWithValue("@USERLOGIN", values[0]);
                cmd.Parameters.AddWithValue("@USERLOGIN", values[1]);
                cmd.Parameters.Add("@USERLOGIN", SqlDbType.VarChar, 10).Value = values[0];
                cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar, 128).Value = values[1];*/
                return cmd;
            }
        }
    }
}
