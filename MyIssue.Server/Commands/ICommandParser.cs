using System.Threading;

namespace MyIssue.Server.Commands
{
    public interface ICommandParser
    {
        void Parser(string input, Client client, CancellationToken ct);
    }
}
