using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public interface IUserCommands
    {
        void WhoAmI(Client client, CancellationToken ct);
        void History(Client client, CancellationToken ct);
        void Disconnect(Client client, CancellationToken ct);
    }
}
