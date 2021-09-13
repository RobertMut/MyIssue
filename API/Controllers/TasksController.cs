using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Converters;
using MyIssue.API.Infrastructure;
using MyIssue.API.Model;
using MyIssue.API.Model.Return;
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
        
        [HttpGet("filter/{all}/{isClosed}/{howMany}/{id?}")]
        public async Task<ActionResult<IEnumerable<TaskReturn>>> GetLastTask(bool all, bool isClosed, int howMany, decimal? id)
        {
            List<Task> tasks = new List<Task>();
            if (all)
            {
                tasks = await _context.Tasks.ToListAsync();
            }
            else
            {
                tasks = await _context.Tasks.Where(d => d.TaskEnd.HasValue == isClosed).ToListAsync();

            }

            if (howMany is not 0)
            {
                tasks = tasks.TakeLast(howMany).ToList();
            }

            if (id is not null)
            {
                tasks = tasks.Where(i => i.TaskId == id.GetValueOrDefault(0)).ToList();
            }

            if (tasks.Count.Equals(0))
            {
                return NotFound();
            }

            List<TaskReturn> returnList = new List<TaskReturn>();
            tasks.ForEach(t => returnList.Add(converter.Convert(t)));
            return returnList;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(decimal id, TaskReturn task)
        {
            var converted = converter.ConvertBack(task);
            if (id != converted.TaskId)
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
    }
}
