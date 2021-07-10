using System.Data.SqlClient;

namespace MyIssue.Core.Interfaces
{
    public interface IUserQueries
    {
        SqlCommand InsertNewTask(string[] values, string table);
        SqlCommand Login(string[] values, string table);
    }
}
