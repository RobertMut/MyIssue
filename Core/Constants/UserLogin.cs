using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Core.Constants
{
    public class UserLogin
    {
        public const string command = "Login\r\n<EOF>\r\n";
        public const string loginParameters = "{0}\r\n<NEXT>\r\n{1}\r\n<EOF>\r\n";
        public const string logout = "Logout\r\n<EOF>\r\n";
    }
}
