using MyIssue.Infrastructure.Database.Models;

namespace MyIssue.Infrastructure.Database
{
    public interface ITaskRepository : IRepository<TASK>
    {
        void InsertTask(string[] input);
    }
}