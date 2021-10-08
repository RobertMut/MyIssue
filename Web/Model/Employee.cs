using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIssue.Web.Model
{
    public class EmployeeRoot
    {
        public List<Employee> Employees { get; set; }
    }
    public class Employee
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string No { get; set; }
        public string Position { get; set; }
    }
}
