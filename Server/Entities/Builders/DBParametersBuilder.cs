using MyIssue.Infrastructure.Entities;

namespace MyIssue.Server.Entities.Builders
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
        public DBParametersTemplate Build() => dBTemp;
        public static DBParametersBuilder Create() => new DBParametersBuilder();

    }
}
