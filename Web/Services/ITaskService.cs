using System.Collections.Generic;
using System.Threading.Tasks;
using Task = MyIssue.Web.Model.Task;

namespace MyIssue.Web.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<Task>> GetTasks();
        Task<IEnumerable<Task>> GetLastTasks(int howMany);
    }
}