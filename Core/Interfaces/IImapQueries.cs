using System.Data.SqlClient;

namespace MyIssue.Core.Interfaces
{
    public interface IImapQueries
    {
        SqlCommand ImapNewTask(string[] values, string table);
    }
}
