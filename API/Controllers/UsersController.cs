using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Infrastructure;
using MyIssue.API.Model;
using MyIssue.API.Services;
using MyIssue.Core.Model.Request;
using MyIssue.Core.Model.Return;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace MyIssue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly MyIssueContext _context;

        public UsersController(MyIssueContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet("GetFull")]
        public async Task<ActionResult<IEnumerable<User>>> GetFullUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("GetFull/{login}")]
        public async Task<ActionResult<User>> GetFullUser(string login)
        {
            var user = await _context.Users.FindAsync(login);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpGet]
        public async Task<ActionResult<UserReturnRoot>> GetUsers()
        {
            List<UserReturn> Users = new List<UserReturn>();
            var users = await _context.Users.ToListAsync();
            users.ForEach(u => Users.Add(new UserReturn
            {
                Username = u.UserLogin
            }));
            return Ok(JsonConvert.SerializeObject(new UserReturnRoot()
            {
                Users = Users
            }));
        }

        // GET: api/Users/{login}
        [HttpGet("{login}")]
        public async Task<ActionResult<User>> GetUser(string login)
        {
            List<UserReturn> Users = new List<UserReturn>();
            var users = await _context.Users.ToListAsync();
            users.ForEach(u => Users.Add(new UserReturn
            {
                Username = u.UserLogin
            }));
            var user = Users.Where(u => u.Username == login);
            if (user.Count().Equals(0)) return NotFound();
            return Ok(JsonConvert.SerializeObject(new UserReturnRoot()
            {
                Users = user.ToList()
            }));
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Post")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserLogin))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.UserLogin }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.UserLogin == id);
        }
    }
}
