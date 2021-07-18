using MyIssue.Core.Entities;
using System.Data.SqlClient;

namespace MyIssue.Core.Interfaces
{
    public interface ISqlCommandParser
    {
        SqlCommand SqlCmdParser(string name, SqlCommandInput query);
    }
}