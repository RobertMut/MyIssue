using System.Threading;
using MyIssue.Core.Entities;

namespace MyIssue.Core.Interfaces
{
    public interface ILogin
    {
        void Login(Client client, CancellationToken ct);
    }

}

