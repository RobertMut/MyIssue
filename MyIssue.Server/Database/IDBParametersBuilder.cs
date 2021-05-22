using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Database
{
    public interface IDBParametersBuilder
    {
        void SetDBAddress(string address);
        void SetUsername(string username);
        void SetPassword(string pass);
        void SetDatabase(string databaseName);
        void SetTaskTable(string taskTableName);
        void SetUsersTable(string usersTableName);
        void SetEmployeesTable(string employeesTableName);
        DBParametersTemplate GetParameters();
    }
}
