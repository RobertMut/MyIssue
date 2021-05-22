using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Database
{
    public class DBParametersBuilder : IDBParametersBuilder
    {
        DBParametersTemplate dBTemp = new DBParametersTemplate();
        public void SetDBAddress(string address)
        {
            dBTemp.DBAddress = address;
        }
        public void SetUsername(string username)
        {
            dBTemp.Username = username;
        }
        public void SetPassword(string pass)
        {
            dBTemp.Password = pass;
        }
        public void SetDatabase(string databaseName)
        {
            dBTemp.Database = databaseName;
        }
        public void SetTaskTable(string taskTableName)
        {
            dBTemp.TaskTable = taskTableName;
        }
        public void SetUsersTable(string usersTableName)
        {
            dBTemp.UsersTable = usersTableName;
        }
        public void SetEmployeesTable(string employeesTableName)
        {
            dBTemp.EmployeesTable = employeesTableName;
        }
        public DBParametersTemplate GetParameters()
        {
            return dBTemp;
        }

    }
}
