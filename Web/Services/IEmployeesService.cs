using System.Threading.Tasks;
using MyIssue.Web.Model;

namespace MyIssue.Web.Services
{
    public interface IEmployeesService
    {
        Task<EmployeeBasicRoot> GetEmployees(string? username, TokenAuth model);
    }
}