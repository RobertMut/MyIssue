using MyIssue.Infrastructure.Model;

namespace MyIssue.Server.Model.Builders
{
    public class DBParametersBuilder : IDBParametersBuilder
    {
        protected ApiParametersTemplate dBTemp;
        private DBParametersBuilder()
        {
            dBTemp = new ApiParametersTemplate();
        }
        public IDBParametersBuilder SetDBAddress(string address)
        {
            dBTemp.ApiAddress = address;
            return this;
        }
        public ApiParametersTemplate Build() => dBTemp;
        public static DBParametersBuilder Create() => new DBParametersBuilder();

    }
}
