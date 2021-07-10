using System.Threading;
using MyIssue.Core.Entities;

namespace MyIssue.Core.Interfaces
{
    public interface IDBCommands
    {
        void CreateTask(Client client, CancellationToken ct);
        void AddEmployee(Client client, CancellationToken ct);
        void AddUser(Client client, CancellationToken ct);
    }
}
