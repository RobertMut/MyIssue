using System.Threading;
using MyIssue.Core.Entities;

namespace MyIssue.Core.Interfaces
{
    public interface ICommandParser
    {
        void Parser(string input, Client client, CancellationToken ct);
    }
}
