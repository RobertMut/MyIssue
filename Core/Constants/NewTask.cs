using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Core.Constants
{
    public class NewTask
    {
        public const string newTask = "CreateTask\r\n<EOF>\r\n";
        public const string newTaskParameters = "{TaskTitle}\r\n<NEXT>\r\n" +
                                                "{TaskDescription}\r\n<NEXT>\r\n" +
                                                "{TaskClient}\r\n<NEXT>\r\n" +
                                                "{TaskAssignment}\r\n<NEXT>\r\n" +
                                                "{TaskOwner}\r\n<NEXT>\r\n" +
                                                "{TaskType}\r\n<NEXT>\r\n" +
                                                "{TaskStart}\r\n<NEXT>\r\n" +
                                                "{TaskEnd}\r\n<NEXT>\r\n" +
                                                "{TaskCreationDate}\r\n<NEXT>\r\n" +
                                                "{CreatedByMail}\r\n<NEXT>\r\n" +
                                                "{TaskDescription}\r\n<EOF>\r\n";
    }
}
