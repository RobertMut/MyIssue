using System.Collections.Generic;

namespace MyIssue.API.Model.Return
{
    public class EmployeeBasicRoot
    {
        public List<EmployeeBasic> Employees { get; set; }
    }
    public class EmployeeBasic
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
    }
}
