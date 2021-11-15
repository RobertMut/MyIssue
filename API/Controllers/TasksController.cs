using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Converters;
using MyIssue.API.Filter;
using MyIssue.API.Helpers;
using MyIssue.API.Infrastructure;
using MyIssue.API.Services;
using MyIssue.API.Wrappers;
using MyIssue.Core.DataTransferObjects.Return;
using Newtonsoft.Json;
using Task = MyIssue.API.Model.Task;

namespace MyIssue.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]

    public class TasksController : ControllerBase
    {
        private readonly MyIssueContext _context;
        private readonly TaskConverter converter;
        private readonly IUriService uriService;
        public TasksController(MyIssueContext context, IUriService service)
        {
            uriService = service;
            _context = context;
            converter = new TaskConverter(context);
        }

        // GET: api/Tasks
        [Microsoft.AspNetCore.Authorization.Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<ActionResult<string>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            List<TaskReturn> tr = new List<TaskReturn>();
            foreach (var task in tasks)
            {
                tr.Add(converter.Convert(task));
            }
            var serialized = JsonConvert.SerializeObject(tr);
            return Content(serialized, "application/json");
        }

        // GET: api/Tasks/5
        [Microsoft.AspNetCore.Authorization.Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult<TaskReturn>> GetTask(decimal id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return converter.Convert(task);
        }
        [Microsoft.AspNetCore.Authorization.Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet("filter/closed={isClosed}&whose={whose}&howmany={howMany}/{id?}")]
        public async Task<IActionResult> GetLastTask(bool isClosed,string whose, int howMany, decimal? id)
        {

            List<Task> tasks;
            if(!isClosed)
            {
                tasks = await _context.Tasks.Where(u => u.TaskEnd == null || u.TaskEnd > DateTime.Now).ToListAsync();

            }
            else
            {
                tasks = await _context.Tasks.Where(u => u.TaskEnd != null || u.TaskEnd <= DateTime.Now).ToListAsync();
            }
            if (!whose.ToLower().Equals("anybody"))
            {
                tasks = tasks.Where(user => user.TaskOwner is not null)
                    .Where(user => user.TaskOwner.ToLower().Equals(whose.ToLower())).ToList();
            }
            if (howMany is not 0)
            {
                tasks = tasks.TakeLast(howMany).ToList();
            }

            if (id is not null)
            {
                tasks = new List<Task>() {_context.Tasks.FirstOrDefault(i => i.TaskId == id.GetValueOrDefault(0))};
            }

            if (tasks.Count().Equals(0))
            {
                return NotFound();
            }

            List<TaskReturn> returnTasks = new List<TaskReturn>();
            tasks.ToList().ForEach(t =>
            {
                returnTasks.Add(converter.Convert(t));
            });
            return Content(JsonConvert.SerializeObject(new TaskReturnRoot()
            {
                Tasks = returnTasks
            }),"application/json");
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Microsoft.AspNetCore.Authorization.Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<IActionResult> PutTask(decimal id, [Microsoft.AspNetCore.Mvc.FromBody]TaskReturn task)
        {
            var converted = converter.ConvertBack(task);
            if (Convert.ToInt32(id) != Convert.ToInt32(converted.TaskId))
            {
                return BadRequest();
            }



            _context.Entry<Model.Task>(converted).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Microsoft.AspNetCore.Authorization.Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult<Task>> PostTask(TaskReturn task)
        {
            var converted = converter.ConvertBack(task);
            _context.Tasks.Add(converted);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = converted.TaskId }, converted);
        }
        [BasicAuth]
        [Microsoft.AspNetCore.Mvc.HttpPost("imap")]
        public async Task<ActionResult<Task>> PostTaskFromImap(TaskReturn task)
        {
            return await PostTask(task);
        }
        // DELETE: api/Tasks/5
        [Microsoft.AspNetCore.Authorization.Authorize]
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(decimal id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        #region Pagination
        [Microsoft.AspNetCore.Authorization.Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet("paged")]
        public async Task<ContentResult> GetPaged([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var response = await _context.Tasks
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Tasks.CountAsync();
            var pagedResponse =
                PaginationHelper.CreatePageResponse<Model.Task>(response, filter, totalRecords, uriService, route);
            return Content(JsonConvert.SerializeObject(pagedResponse), "application/json");
        }

        #endregion


        private bool TaskExists(decimal id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }

    }
}
