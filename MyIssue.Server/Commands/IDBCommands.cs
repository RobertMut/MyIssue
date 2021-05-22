using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyIssue.Server
{
    public interface IDBCommands
    {
        void CreateTask(Client client, CancellationToken ct);
    }
}
