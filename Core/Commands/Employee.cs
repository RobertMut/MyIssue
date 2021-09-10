using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormatWith;
using MyIssue.Core.Constants;
using MyIssue.Core.String;

namespace MyIssue.Core.Commands
{
    public class Employee
    {
        public static IEnumerable<byte[]> New<T>(T entity)
        {
            return new List<byte[]>()
            {
                StringStatic.ByteMessage(EmployeeConstants.newEmployee),
                StringStatic.ByteMessage(EmployeeConstants.employeeParameters.FormatWith(entity))
            };
        }

        public static IEnumerable<byte[]> GetEmployeeByName(string name)
        {
            return new List<byte[]>()
            {
                StringStatic.ByteMessage(EmployeeConstants.getEmployeeByName),
                StringStatic.ByteMessage("{0}\r\n<EOF>\r\n")
            };
        }
    }
}
