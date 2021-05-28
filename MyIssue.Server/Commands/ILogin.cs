using System.Threading;

namespace MyIssue.Server.Commands
{
    public interface ILogin
    {
        void Login(Client client, CancellationToken ct);
    }

}

