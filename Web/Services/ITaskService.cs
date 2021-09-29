using System.Collections.Generic;
using System.Threading.Tasks;
using MyIssue.Web.Model;
using Task = MyIssue.Web.Model.Task;

namespace MyIssue.Web.Services
{
    public interface ITaskService
    {
        Task<string> CreateTask(Task task, TokenAuth model);
        Task<TaskRoot> GetTasks(bool isClosed, bool all, string whoseTasks, int howMany, int? id, TokenAuth model);
        Task<bool> PutTask(Task task, TokenAuth model);
    }
}   