using MyIssue.Core.Entities;

namespace MyIssue.Core.Interfaces
{
    public interface IDBParameters
    {
        DBParametersTemplate Parameters(IDBParametersBuilder _builder, string database, string address, string username, string password, string taskTable, string userTable, string employeesTable);
    }
}
