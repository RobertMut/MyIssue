using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Options;
using MyIssue.Core.Model.Return;
using MyIssue.Web.Helpers;
using MyIssue.Web.Model;
using MyIssue.Web.Services;

namespace MyIssue.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;
        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet("someClosed/{whoseTasks}/{howMany}")]
        public async Task<TaskReturnRoot> GetSomeClosed(int howMany, string whoseTasks)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetTasks(true, false, whoseTasks, howMany, null, auth);
        }
        [HttpGet("{id}")]
        public async Task<TaskReturnRoot> GetById(int id)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetTasks(false, true, "anybody", 0, id, auth);
        }
        [HttpGet("someOpen/{whoseTasks}/{howMany}")]
        public async Task<TaskReturnRoot> GetSomeOpen(int howMany, string whoseTasks)
        {

            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetTasks(false, false, whoseTasks, howMany, null, auth);
        }
        [HttpGet("user/{whoseTasks}")]
        public async Task<TaskReturnRoot> GetByUser(string whoseTasks)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            return await _service.GetTasks(false, true, whoseTasks, 0, null, auth);
        }

        #region Pagination

        [HttpPost("pagedFirst")]
        public async Task<ActionResult<PageResponse<TaskReturn>>> GetPagedInitial([FromBody]PageRequest request)
        {
            var token = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            var result = await _service.FirstPagedGet(request.Page, request.Size, token);
            return result;
        }

        [HttpPost("pagedLink")]
        public async Task<ActionResult<PageResponse<TaskReturn>>> GetPagedLink([FromBody] PageRequest request)
        {
            var token = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            var result = await _service.PagedLinkGet(request.Link, token);
            return result;
        }
        #endregion
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask([FromBody] TaskReturn task)
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var auth = new TokenAuth(token);
            bool result = await _service.PutTask(task, auth);
            if (!result) return BadRequest();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> NewTask([FromBody] TaskReturn task)
        {
            var auth = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            string result = await _service.CreateTask(task, auth);
            if (result.Contains("CreatedAtAction")) return CreatedAtAction("NewTask", result);
            return BadRequest(result);
        }

    }
}
