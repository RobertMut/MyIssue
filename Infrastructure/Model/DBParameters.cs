using System.Data.SqlClient;

namespace MyIssue.Infrastructure.Model
{
    public static class DBParameters
    {
        public static DBParametersTemplate Parameters { get; set; }
        public static SqlConnectionStringBuilder ConnectionString { get; set; }
    }
}
