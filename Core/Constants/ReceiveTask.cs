using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Core.Constants
{
    public class ReceiveTask
    {
        public const string getTask = "GetTask\r\n<EOF>\r\n";
        public const string getTaskById = "GetTaskById\r\n<EOF>\r\n";
        public const string getTaskByRange = "GetTaskByRange\r\n<EOF\r\n";
    }
}
