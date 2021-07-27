using MyIssue.Infrastructure.Database.Models;

namespace MyIssue.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ITaskRepository Task { get; }
        IUserRepository User { get; }
        IEmployeeRepository Employee { get; }
        IClientRepository Client { get; }

        int Complete();
        void Dispose();
    }
}