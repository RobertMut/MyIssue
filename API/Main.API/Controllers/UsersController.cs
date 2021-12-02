using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Main.API.Infrastructure;
using MyIssue.Main.API.Model;
using Newtonsoft.Json;

namespace MyIssue.Main.API.Controllers
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

            user.Password = null;
            return Ok(user);
        }
        [HttpGet]
        public async Task<ActionResult<UserReturnRoot>> GetUsers()
        {
            List<UserReturn> Users = new List<UserReturn>();
            var users = await _context.Users.ToListAsync();
            users.ForEach(u => Users.Add(new UserReturn
            {
                Username = u.UserLogin,
                Password = null,
                Type = _context.UserTypes.First(t => t.Id == u.UserType).Name,

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
        [HttpPost()]
        public async Task<ActionResult<User>> PostUser(UserReturn user)
        {
            _context.Users.Add(new User
            {
                UserLogin = user.Username,
                Password = user.Password,
                UserType = _context.UserTypes.First(u => u.Name == user.Type).Id,
            });
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeName == user.Username);
            try
            {

                if (employee is not null)
                    _context.EmployeeUser.Add(new EmployeeUser
                    {
                        UserLogin = user.Username,
                        EmployeeLogin = user.Username,
                    });
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { UserLogin = user.Username }, user);
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
