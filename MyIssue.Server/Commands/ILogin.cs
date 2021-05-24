using System;
using System.Threading;

namespace MyIssue.Server
{
    public interface ILogin
    {
        void Login(Client client, CancellationToken ct);
    }

}

