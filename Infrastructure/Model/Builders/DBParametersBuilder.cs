namespace MyIssue.Infrastructure.Model.Builders
{
    public class ApiParametersBuilder : IApiParametersBuilder
    {
        protected ApiParametersTemplate dBTemp;
        private ApiParametersBuilder()
        {
            dBTemp = new ApiParametersTemplate();
        }
        public IApiParametersBuilder SetApiAddress(string address)
        {
            dBTemp.ApiAddress = address;
            return this;
        }
        public ApiParametersTemplate Build() => dBTemp;
        public static ApiParametersBuilder Create() => new ApiParametersBuilder();

    }
}
