using System.Data.SqlClient;
namespace MyIssue.Server.Database
{
    public interface IAdminQueries
    {
        SqlCommand AddEmployee(string[] values, string table);
        SqlCommand AddUser(string[] values, string table);
        SqlCommand BanUser(string name, string table);
        SqlCommand DeleteUser(string name, string table);
    }
}
