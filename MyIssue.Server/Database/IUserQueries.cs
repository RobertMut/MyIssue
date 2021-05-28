using System.Data.SqlClient;

namespace MyIssue.Server.Database
{
    public interface IUserQueries
    {
        SqlCommand InsertNewTask(string[] values, string table);
        SqlCommand Login(string[] values, string table);
    }
}
