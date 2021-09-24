using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MyIssue.Web.Helpers;
using MyIssue.Web.Model;
using MyIssue.Web.Services;
using Task = MyIssue.Web.Model.Task;

namespace MyIssue.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[CustomAuthorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;
        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet("someClosed/{whoseTasks}/{howMany}")]
        public async Task<TaskRoot> GetSomeClosed(int howMany, string whoseTasks)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetTasks(true, false, whoseTasks, howMany, null, auth);
        }
        [HttpGet("{id}")]
        public async Task<TaskRoot> GetById(int id)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetTasks(false, true, "anybody", 0, id, auth);
        }
        [HttpGet("someOpen/{whoseTasks}/{howMany}")]
        public async Task<TaskRoot> GetSomeOpen(int howMany, string whoseTasks)
        {

            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetTasks(false, false, whoseTasks, howMany, null, auth);
        }
        [HttpGet("user/{whoseTasks}")]
        public async Task<TaskRoot> GetByUser(string whoseTasks)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetTasks(false, true, whoseTasks, 0, null, auth);
        }
    }
}
