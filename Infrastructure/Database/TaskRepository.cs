using MyIssue.Core.Entities.Database;
using MyIssue.Core.Interfaces;
using System;

namespace MyIssue.Infrastructure.Database
{
    public class TaskRepository : Repository<TASK>, ITaskRepository
    {
        public TaskRepository(MyIssueDatabase context) : base(context)
        {

        }
        public void InsertTask(string[] input)
        {
            _context.TASKS.Add(new TASK
            {
                taskTitle = input[0],
                taskDesc = input[1],
                taskStart = Convert.ToDateTime(input[2]),
                taskClient = Decimal.Parse(input[3]),
                taskType = Decimal.Parse(input[4]),
                mailId = input.Length.Equals(6) ? input[6] : DBNull.Value.ToString()
            });
        }
    }
}
