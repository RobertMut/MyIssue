using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIssue.Web.Model
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
