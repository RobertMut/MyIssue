using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIssue.API.Model.Return
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
