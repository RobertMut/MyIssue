using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyIssue.Web.Model;
using MyIssue.Web.Services;

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
        public async Task<EmployeeBasicRoot> Get()
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            Console.WriteLine("TOKEN   " + token);
            var auth = new TokenAuth(token);
            return await _service.GetEmployees(null, auth);
        }
        [HttpGet("{username}")]
        public async Task<EmployeeBasicRoot> GetUserByName(string username)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            Console.WriteLine("TOKEN   " + token);
            var auth = new TokenAuth(token);
            return await _service.GetEmployees(username, auth);
        }
    }
}