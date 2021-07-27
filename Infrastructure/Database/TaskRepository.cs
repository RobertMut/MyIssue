using MyIssue.Core.Entities.Database;
using MyIssue.Core.Interfaces;
using System;
using System.Linq;
using System.Threading;

namespace MyIssue.Infrastructure.Database
{
    public class TaskRepository : Repository<TASK>, ITaskRepository
    {
        public TaskRepository(MyIssueDatabase context) : base(context)
        {

        }
        public void InsertTask(string[] input, decimal? client)
        {
            var task = new TASK
            {
                taskTitle = input[0],
                taskDesc = input[1],
                taskCreation = DateTime.Parse(input[2]),
                taskClient = client,
                taskType = Decimal.Parse(input[4])
            };
        }
    }
}
