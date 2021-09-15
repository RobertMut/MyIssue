using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MyIssue.Web.Model;
using MyIssue.Web.Services;
using Task = MyIssue.Web.Model.Task;

namespace MyIssue.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;
        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet("someClosed/{howMany}")]
        public async Task<IEnumerable<Task>> GetSomeClosed(int howMany)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetTasks(true, false, howMany, null,auth);
        }
        [HttpGet("{id}")]
        public async Task<IEnumerable<Task>> GetClosedById(int id)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetTasks(false, true, 0, id, auth);
        }
        [HttpGet("someOpen/{howMany}")]
        public async Task<IEnumerable<Task>> GetSomeOpen(int howMany, int? id)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetTasks(false, false, howMany, null, auth);
        }
        [HttpGet]
        public async Task<IEnumerable<Task>> Get()
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return  await _service.GetTasks(false, true, 0, null, auth);
        }
    }
}
