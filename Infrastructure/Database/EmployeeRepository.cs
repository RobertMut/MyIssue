using MyIssue.Core.Entities.Database;
using MyIssue.Core.Interfaces;
using System;

namespace MyIssue.Infrastructure.Database
{
    public class EmployeeRepository : Repository<EMPLOYEE>, IEmployeeRepository
    {
        public EmployeeRepository(MyIssueDatabase context) : base(context)
        {

        }
        public void AddNewEmployee(string[] input)
        {
            _context.EMPLOYEES.Add(
                new EMPLOYEE
                {
                    employeeLogin = input[0],
                    employeeName = input[1],
                    employeeSurname = input[2],
                    employeeNo = input[3],
                    employeePosition = string.IsNullOrEmpty(input[4]) ? -1 : Decimal.Parse(input[4])
                }
            );
        }
    }
}
