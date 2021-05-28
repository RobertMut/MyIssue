using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Database
{
    public interface IDBConnector
    {
        void MakeWriteQuery(SqlConnectionStringBuilder sqlConnection, SqlCommand command);
        DataTable MakeReadQuery(SqlConnectionStringBuilder sqlConnection, SqlCommand command);
    }
}
