using System.Collections.Generic;

namespace MyIssue.Core.DataTransferObjects.Return
{
    public class TaskTypeReturnRoot
    {
        public List<TaskTypeReturn> TaskTypes { get; set; }
    }
    public class TaskTypeReturn
    {
        public string TaskType { get; set; }
    }
}
