using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MyIssue.Web.Services;
using Task = MyIssue.Web.Model.Task;

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

        [HttpGet("all")]
        public IEnumerable<Task> Get()
        {
            
            return _service.GetTasks().GetAwaiter().GetResult();
        }

        [HttpGet("{numberOfTasks}")]
        public IEnumerable<Task> GetLast(int numberOfTasks)
        {
            return _service.GetLastTasks(numberOfTasks).GetAwaiter().GetResult();

        }
    }
}
