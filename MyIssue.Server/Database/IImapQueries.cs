using System.Data.SqlClient;

namespace MyIssue.Server.Database
{
    interface IImapQueries
    {
        SqlCommand ImapNewTask(string[] values, string table);
    }
}
