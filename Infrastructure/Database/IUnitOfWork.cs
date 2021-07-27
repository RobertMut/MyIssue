using MyIssue.Infrastructure.Database.Models;

namespace MyIssue.Infrastructure.Database
{
    public interface IUnitOfWork
    {
        ITaskRepository Task { get; }
        IUserRepository User { get; }
        IEmployeeRepository Employee { get; }

        int Complete();
        void Dispose();
    }
}