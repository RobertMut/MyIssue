namespace MyIssue.Infrastructure.Model.Builders
{
    public interface IApiParametersBuilder
    {
        IApiParametersBuilder SetApiAddress(string address);
        ApiParametersTemplate Build();
    }
}
