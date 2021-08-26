using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Infrastructure;
using MyIssue.API.Model.Return;
using Task = MyIssue.API.Model.Task;

namespace MyIssue.API.Converters
{
    public class TaskConverter
    {
        public MyIssueContext _context;
        public TaskConverter(MyIssueContext context)
        {
            _context = context;
        }

        public TaskReturn Convert(Task task)
        {
            return new TaskReturn
            {
                TaskId = task.TaskId,
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDesc,
                TaskClient = _context.Clients.FirstOrDefaultAsync(c => c.ClientId.Equals(task.TaskId))
                    .Result
                    .ClientName,
                TaskAssignment = task.TaskAssignment,
                TaskOwner = task.TaskOwner,
                TaskType = _context.TaskTypes.FirstOrDefaultAsync(tt => tt.TypeId.Equals(task.TaskType))
                    .Result
                    .TypeName,
                TaskStart = task.TaskStart,
                TaskEnd = task.TaskEnd,
                TaskCreationDate = task.TaskCreation,
                CreatedByMail = task.MailId
            };
        }

        public Task ConvertBack(TaskReturn taskReturn)
        {
            return new Task
            {
                TaskId = taskReturn.TaskId,
                TaskTitle = taskReturn.TaskTitle,
                TaskDesc = taskReturn.TaskDescription,
                TaskClient = _context.Clients.FirstOrDefaultAsync(c => c.ClientName.Equals(taskReturn.TaskClient)).Result.ClientId,
                TaskOwner = taskReturn.TaskOwner,
                TaskAssignment = taskReturn.TaskAssignment,
                TaskType = _context.TaskTypes.FirstOrDefaultAsync(tt => tt.TypeName.Equals(taskReturn.TaskType)).Result.TypeId,
                TaskStart = taskReturn.TaskStart,
                TaskEnd = taskReturn.TaskStart,
                TaskCreation = taskReturn.TaskCreationDate,
                MailId = taskReturn.CreatedByMail is null ? DBNull.Value.ToString() : taskReturn.CreatedByMail
            };
        }
    }
}
