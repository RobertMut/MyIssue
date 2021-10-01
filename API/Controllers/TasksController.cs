using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Converters;
using MyIssue.API.Infrastructure;
using MyIssue.API.Model;
using MyIssue.API.Model.Return;
using Newtonsoft.Json;
using Task = MyIssue.API.Model.Task;

namespace MyIssue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly MyIssueContext _context;
        private readonly TaskConverter converter;
        public TasksController(MyIssueContext context)
        {
            _context = context;
            converter = new TaskConverter(context);
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskReturn>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            List<TaskReturn> tr = new List<TaskReturn>();
            foreach (var task in tasks)
            {
                tr.Add(converter.Convert(task));
            }

            return tr;
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskReturn>> GetTask(decimal id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return converter.Convert(task);
        }
        
        [HttpGet("filter/all={all}&closed={isClosed}&whose={whose}&howmany={howMany}/{id?}")]
        public async Task<IActionResult> GetLastTask(bool all, bool isClosed,string whose, int howMany, decimal? id)
        {

            List<Task> tasks;
            if (all)
            {
                tasks = await _context.Tasks.ToListAsync();
            }
            else
            {
                tasks = await _context.Tasks.Where(d => (d.TaskEnd == null || d.TaskEnd > DateTime.Now).Equals(!isClosed)).ToListAsync();

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
                tasks = new List<Task>() {tasks.FirstOrDefault(i => i.TaskId == id.GetValueOrDefault(0))};
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
            return Ok(JsonConvert.SerializeObject(new TaskReturnRoot()
            {
                Tasks = returnTasks
            }));
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(decimal id, [FromBody]TaskReturn task)
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
        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(TaskReturn task)
        {
            var converted = converter.ConvertBack(task);
            _context.Tasks.Add(converted);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = converted.TaskId }, converted);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
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

        private bool TaskExists(decimal id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }

        #region Pagination


        #endregion



    }
}
