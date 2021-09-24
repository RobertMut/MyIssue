using System.Collections.Generic;
using System.Threading.Tasks;
using MyIssue.Web.Model;
using Task = MyIssue.Web.Model.Task;

namespace MyIssue.Web.Services
{
    public interface ITaskService
    {
        Task<TaskRoot> GetTasks(bool isClosed, bool all, string whoseTasks, int howMany, int? id, TokenAuth model);
    }
}   