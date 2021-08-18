using MyIssue.Infrastructure.Database.Models;

namespace MyIssue.Core.Interfaces
{
    public interface IUnitOfWork
    {
        int Complete();
        void Dispose();
    }
}