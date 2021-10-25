using System.Collections.Generic;

namespace MyIssue.Core.Model.Return
{
    public class UserReturnRoot
    {
        public List<UserReturn> Users { get; set; }
    }
    public class UserReturn
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
    }
}