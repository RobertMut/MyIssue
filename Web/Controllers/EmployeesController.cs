using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Web.Helpers;
using MyIssue.Web.Model;
using MyIssue.Web.Services;
using Newtonsoft.Json;

namespace MyIssue.Web.Controllers
{
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _service;
        public EmployeesController(IEmployeesService service)
        {
            _service = service;
        }
        // GET
        [HttpGet]
        public async Task<EmployeeReturnRoot> Get()
        {
            var auth = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            string result = await _service.GetEmployees(null, auth);;
            return JsonConvert.DeserializeObject<EmployeeReturnRoot>(result);
        }
        [HttpGet("{username}")]
        public async Task<EmployeeReturnRoot> GetUserByName(string username)
        {
            var auth = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            string result = await _service.GetEmployees(username, auth);
            return JsonConvert.DeserializeObject<EmployeeReturnRoot>(result);
        }
        [HttpPost]
        public async Task<string> NewEmployee([FromBody] EmployeeReturn employee)
        {
            var auth = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            string result = await _service.CreateEmployee(employee, auth);
            return result;
        }
    }
}