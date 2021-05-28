using System.Threading;

namespace MyIssue.Server.Commands
{
    public interface IDBCommands
    {
        void CreateTask(Client client, CancellationToken ct);
        void AddEmployee(Client client, CancellationToken ct);
        void AddUser(Client client, CancellationToken ct);
    }
}
