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
using MyIssue.API.Model.Request;
using MyIssue.API.Model.Return;
using MyIssue.API.Services;

namespace MyIssue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly MyIssueContext _context;
        private IUserService _userService;

        public UsersController(MyIssueContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("Get/{login}")]
        public async Task<ActionResult<User>> GetUser(string login)
        {
            var user = await _context.Users.FindAsync(login);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthRequest model)
        {
            Console.WriteLine(nameof(this.Authenticate));
            var response = _userService.AuthenticateUser(model);
            if (response is null)
                return BadRequest(new {message = "Username or password in incorrect"});
            Console.WriteLine(response);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("tokenauthenticate")]
        public IActionResult AuthenticateToken([FromBody]AuthTokenRequest model)
        {
            Console.WriteLine(nameof(this.AuthenticateToken));
            bool verified = _userService.VerifyToken(model.Token);
            string username = _userService.GetClaim(model.Token, "username");
            if (verified && username.Equals(model.Username))
            {
                var user = _context.Users.First(u => u.UserLogin.Equals(username));
                return Ok(new Authenticate(user, model.Token));
            }

            return BadRequest(new { message = "Token is invalid" });
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public IActionResult Logout([FromBody]Token token)
        {
            Console.WriteLine(nameof(this.Logout));
            string response = _userService.RevokeToken(token.TokenString);
            if (response is not null) return Ok(response);
            return BadRequest();
        }
        
        // [Authorize]
        // [HttpGet]
        // public IActionResult GetAll()
        // {
        //     var users = _userService.GetAll();
        //     return Ok(users);
        // }
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Put/{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.UserLogin)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
