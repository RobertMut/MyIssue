using System.Collections.Generic;
using System.Threading.Tasks;
using MyIssue.Web.Model;
using Task = MyIssue.Web.Model.Task;

namespace MyIssue.Web.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<Task>> GetTasks(bool isClosed, bool all, int howMany, int? id, TokenAuth model);
    }
}   