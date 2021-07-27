using MyIssue.Core.Entities.Database;

namespace MyIssue.Core.Interfaces
{
    public interface ITaskRepository : IRepository<TASK>
    {
        void InsertTask(string[] input);
    }
}