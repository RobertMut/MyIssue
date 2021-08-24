using System.Data.SqlClient;

namespace MyIssue.Infrastructure.Entities
{
    public static class DBParameters
    {
        public static DBParametersTemplate Parameters { get; set; }
        public static SqlConnectionStringBuilder ConnectionString { get; set; }
    }
}
