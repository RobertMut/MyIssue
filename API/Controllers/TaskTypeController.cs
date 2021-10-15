using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Infrastructure;
using MyIssue.Core.Model.Return;
using Newtonsoft.Json;

namespace MyIssue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskTypeController : ControllerBase
    {
        private readonly MyIssueContext _context;

        public TaskTypeController(MyIssueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<TaskTypeReturnRoot>> GetTaskTypes()
        {

            List<TaskTypeReturn> taskTypeList = new List<TaskTypeReturn>();
            var tasktypes = await _context.TaskTypes.ToListAsync();
            tasktypes.ForEach(e => taskTypeList.Add(new TaskTypeReturn
            {
                TaskType = e.TypeName
            }));
            return Ok(JsonConvert.SerializeObject(new TaskTypeReturnRoot()
            {
                TaskTypes = taskTypeList
            }));
        }
    }
}
