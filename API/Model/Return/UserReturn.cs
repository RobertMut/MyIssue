using System.Collections.Generic;

namespace MyIssue.API.Model.Return
{
    public class UserReturnRoot
    {
        public List<UserReturn> Users { get; set; }
    }
    public class UserReturn
    {
        public string Username { get; set; }
    }
}