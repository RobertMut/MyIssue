using MyIssue.Core.Entities;
namespace MyIssue.Core.Interfaces
{
    public interface IImapParametersBuilder
    {
        IImapParametersBuilder SetAddress(string address);
        IImapParametersBuilder SetPort(int port);
        IImapParametersBuilder SetSocketOptions(string socketOptions);
        IImapParametersBuilder SetLogin(string login);
        IImapParametersBuilder SetPassword(string password);
        ImapParametersTemplate Build();
    }
}
