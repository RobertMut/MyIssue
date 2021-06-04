using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Database
{
    public class DBParametersTemplate
    {
        public string DBAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string TaskTable { get; set; }
        public string UsersTable { get; set; }
        public string EmployeesTable { get; set; }
        public string ClientsTable { get; set; }
    }
}
