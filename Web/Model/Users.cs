using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIssue.Web.Model
{
    public class UsersRoot
    {
        public List<Users> Users { get; set; }
    }
    public class Users
    {
        public string Username { get; set; }
    }
}
