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
        private IUserService _userService;

        public UsersController(MyIssueContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
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
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthRequest model)
        {
            Console.WriteLine(nameof(this.Authenticate));
            var response = _userService.AuthenticateUser(model);
            if (response is null)
                return BadRequest(new {message = "User or password in incorrect"});
            Console.WriteLine(response);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("tokenauthenticate")]
        public IActionResult AuthenticateToken([FromBody] AuthTokenRequest model)
        {
            bool verified = _userService.VerifyToken(model.Token);
            string username = _userService.GetClaim(model.Token, "username");
            if (verified && username.Equals(model.Username))
            {
                var user = _context.Users.First(u => u.UserLogin.Equals(username));
                return Ok(new Authenticate(user, model.Token));
            }

            return BadRequest(new {message = "Token is invalid"});
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
        
        [Authorize]
        [HttpPost("checktoken")]
        public IActionResult CheckToken([FromBody] Token token)
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Put/{login}")]
        public async Task<IActionResult> PutUser(string login, [FromBody]Password user)
        {
            if (login != user.UserLogin)
            {
                return BadRequest();
            }

            var foundUser = _context.Users.Single(u => u.UserLogin == user.UserLogin);
            if (foundUser is not null && foundUser.Password == user.OldPassword)
            {
                foundUser.Password = user.NewPassword;
                _context.Entry(foundUser).State = EntityState.Modified;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(login))
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
