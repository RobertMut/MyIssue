using System.Data.SqlClient;

namespace MyIssue.Server.Database
{
    public static class DBParameters
    {
        public static DBParametersTemplate Parameters { get; set; }
        public static SqlConnectionStringBuilder ConnectionString { get; set; }
    }
}
