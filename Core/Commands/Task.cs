using System.Collections.Generic;
using FormatWith;
using MyIssue.Core.Constants;
using MyIssue.Core.String;

namespace MyIssue.Core.Commands
{
    public class Task
    {
        public static IEnumerable<byte[]> New<T>(T entity)
        {
            return new List<byte[]>()
            {
                StringStatic.ByteMessage(TaskConstants.newTask),
                StringStatic.ByteMessage(TaskConstants.newTaskParameters.FormatWith(entity))
            };
        }

        public static IEnumerable<byte[]> GetTask()
        {
            return new List<byte[]>()
            {
                StringStatic.ByteMessage(TaskConstants.getTask)
            };
        }
        public static IEnumerable<byte[]> GetTaskById(int id)
        {
            return new List<byte[]>()
            {
                StringStatic.ByteMessage(TaskConstants.getTaskById),
                StringStatic.ByteMessage($"{id}\r\n<EOF>\r\n")
            };
        }
        public static IEnumerable<byte[]> GetLastTask(int howMany)
        {
            return new List<byte[]>()
            {
                StringStatic.ByteMessage(TaskConstants.getLastTask),
                StringStatic.ByteMessage($"{howMany}\r\n<EOF>\r\n")
            };
        }
    }
}
