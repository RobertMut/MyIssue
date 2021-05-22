using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Database
{
    public class DBParameterCoordinator
    {
        public DBParametersTemplate Parameters(IDBParametersBuilder _builder, string database, string address, string username, string password, string taskTable, string userTable, string employeesTable)
        {
            _builder.SetDatabase(database);
            _builder.SetDBAddress(address);
            _builder.SetUsername(username);
            _builder.SetPassword(password);
            _builder.SetTaskTable(taskTable);
            _builder.SetUsersTable(userTable);
            _builder.SetEmployeesTable(employeesTable);
            return _builder.GetParameters();
        }
    }
}
