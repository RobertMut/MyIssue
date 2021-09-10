namespace MyIssue.Core.Constants
{
    public class EmployeeConstants
    {
        public const string newEmployee = "AddEmployee\r\n<EOF>\r\n";

        public const string employeeParameters = "{EmployeeLogin}\r\n<NEXT>\r\n" +
                                                "{EmployeeName}\r\n<NEXT>\r\n" +
                                                "{EmployeeSurname}\r\n<NEXT>\r\n" +
                                                "{EmployeeNo}\r\n<NEXT>\r\n" +
                                                "{EmployeePosition}\r\n<EOF>\r\n";
        public const string getEmployeeByName = "GetTaskByLogin\r\n<EOF>\r\n";
    }
}