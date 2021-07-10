using System.Data;
using System.Data.SqlClient;
namespace MyIssue.Server.Database
{
    public interface IDBConnector
    {
        void MakeWriteQuery(SqlConnectionStringBuilder sqlConnection, SqlCommand command);
        DataTable MakeReadQuery(SqlConnectionStringBuilder sqlConnection, SqlCommand command);
    }
}
