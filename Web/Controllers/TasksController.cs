using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
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
            return await _service.GetTasks(true, false, howMany, null);
        }
        [HttpGet("{id}")]
        public async Task<IEnumerable<Task>> GetClosedById(int id)
        {
            return _service.GetTasks(false, true, 0, id).GetAwaiter().GetResult();
        }
        [HttpGet("someOpen/{howMany}")]
        public async Task<IEnumerable<Task>> GetSomeOpen(int howMany, int? id)
        {
            return _service.GetTasks(false, false, howMany, null).GetAwaiter().GetResult();
        }
        [HttpGet]
        public async Task<IEnumerable<Task>> Get()
        {
            return _service.GetTasks(false, true, 0, null).GetAwaiter().GetResult();
        }
    }
}
