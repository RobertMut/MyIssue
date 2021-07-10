using MyIssue.Core.Interfaces;
using MyIssue.Core.Entities;
namespace MyIssue.Server.Database
{
    public class DBParametersBuilder : IDBParametersBuilder
    {
        protected DBParametersTemplate dBTemp;
        private DBParametersBuilder()
        {
            dBTemp = new DBParametersTemplate();
        }
        public IDBParametersBuilder SetDBAddress(string address)
        {
            dBTemp.DBAddress = address;
            return this;
        }
        public IDBParametersBuilder SetUsername(string username)
        {
            dBTemp.Username = username;
            return this;
        }
        public IDBParametersBuilder SetPassword(string pass)
        {
            dBTemp.Password = pass;
            return this;
        }
        public IDBParametersBuilder SetDatabase(string databaseName)
        {
            dBTemp.Database = databaseName;
            return this;
        }
        public IDBParametersBuilder SetTaskTable(string taskTableName)
        {
            dBTemp.TaskTable = taskTableName;
            return this;
        }
        public IDBParametersBuilder SetUsersTable(string usersTableName)
        {
            dBTemp.UsersTable = usersTableName;
            return this;
        }
        public IDBParametersBuilder SetEmployeesTable(string employeesTableName)
        {
            dBTemp.EmployeesTable = employeesTableName;
            return this;
        }
        public IDBParametersBuilder SetClientsTable(string clientsTableName)
        {
            dBTemp.ClientsTable = clientsTableName;
            return this;
        }
        public DBParametersTemplate Build() => dBTemp;
        public static DBParametersBuilder Create() => new DBParametersBuilder();

    }
}
