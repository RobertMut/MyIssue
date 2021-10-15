using MyIssue.Infrastructure.Model;

namespace MyIssue.Server.Model.Builders
{
    public interface IDBParametersBuilder
    {
        IDBParametersBuilder SetDBAddress(string address);
        ApiParametersTemplate Build();
    }
}
