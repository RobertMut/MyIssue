using System.Collections.Generic;

namespace MyIssue.API.Model.Return
{
    public class EmployeeBasicRoot
    {
        public List<EmployeeReturn> Employees { get; set; }
    }
    public class EmployeeReturn
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string No { get; set; }
        public string Position { get; set; }
    }
}
