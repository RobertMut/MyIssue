using System;
using System.Threading;

namespace MyIssue.Server
{
    public interface ILogin
    {
        void Login(string input, Client client, CancellationToken ct);
    }

}

