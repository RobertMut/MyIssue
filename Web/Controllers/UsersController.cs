using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyIssue.Web.Helpers;
using MyIssue.Web.Model;
using MyIssue.Web.Services;

namespace MyIssue.Web.Controllers
{
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;
        public UsersController(IUsersService service)
        {
            _service = service;
        }
        // GET
        [HttpGet]
        public async Task<UsersRoot> Get()
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetUsers(null, auth);
        }
        [HttpGet("{name}")]
        public async Task<UsersRoot> GetUserByName(string name)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetUsers(name, auth);
        }
        [HttpPost("{name}")]
        public async Task<IActionResult> SetNewPassword([FromBody] Password password)
        {
            var auth = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            var response = await _service.ChangePassword(password, auth);
            return Ok(response);
        }
    }
}