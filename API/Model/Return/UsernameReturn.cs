using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIssue.API.Model.Return
{
    public class UsernameReturnRoot
    {
        public List<UsernameReturn> Users { get; set; }
    }
    public class UsernameReturn
    {
        public string Username { get; set; }
    }
}
