﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            if (task is null)
            {
                return new TaskReturn();
            }

            var type = _context.TaskTypes.FirstOrDefaultAsync(tt => tt.TypeId == task.TaskType).Result.TypeName;
            var client = _context.Clients.FirstOrDefaultAsync(c => c.ClientId == task.TaskClient).Result.ClientName;
            return new TaskReturn
            {
                TaskId = task.TaskId,
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDesc,
                TaskClient = client,
                TaskAssignment = task.TaskAssignment,
                TaskOwner = task.TaskOwner,
                TaskType = type,
                TaskStart = task.TaskStart,
                TaskEnd = task.TaskEnd,
                TaskCreationDate = task.TaskCreation,
                CreatedByMail = task.MailId
            };

        }

        public Task ConvertBack(TaskReturn taskReturn)
        {
            if (taskReturn is null)
            {
                return new Task();
            }
            decimal type = _context.TaskTypes.FirstOrDefaultAsync(tt => tt.TypeName.Equals(taskReturn.TaskType)).GetAwaiter().GetResult().TypeId;
            decimal client = _context.Clients.FirstOrDefaultAsync(c => c.ClientName.Equals(taskReturn.TaskClient))
                .GetAwaiter().GetResult().ClientId;
            return new Task
            {
                TaskId = taskReturn.TaskId,
                TaskTitle = taskReturn.TaskTitle,
                TaskDesc = taskReturn.TaskDescription,
                TaskClient = client,
                TaskOwner = taskReturn.TaskOwner,
                TaskAssignment = taskReturn.TaskAssignment,
                TaskType = type,
                TaskStart = taskReturn.TaskStart,
                TaskEnd = taskReturn.TaskStart,
                TaskCreation = taskReturn.TaskCreationDate,
                MailId = taskReturn.CreatedByMail is null ? DBNull.Value.ToString() : taskReturn.CreatedByMail
            };
        }
    }
}
