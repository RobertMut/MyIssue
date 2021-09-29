using System.Collections.Generic;

namespace MyIssue.Web.Model
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
