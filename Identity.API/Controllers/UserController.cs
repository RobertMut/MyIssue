using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIssue.Core.DataTransferObjects.Request;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Identity.API.Infrastructure;
using MyIssue.Identity.API.Model;
using MyIssue.Identity.API.Services;
using Newtonsoft.Json;

namespace MyIssue.Identity.API.Controllers
{
    [Route("Users")]
    public class UserController : ControllerBase
    {
        private IdentityContext _context;
        private readonly IAuthService _service;

        public UserController(IdentityContext context, IAuthService service)
        {
            _context = context;
            _service = service;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetUsersData()
        {
            var users = await _context.Users.Select(u => new UserReturn()
            {
                Username = u.UserLogin,
                Type = u.Password
            }).ToListAsync(); ;
            return Ok(JsonConvert.SerializeObject(users));
        }
        [HttpGet("{login}")]
        public async Task<IActionResult> GetUser(string login)
        { 
            var user = await _context.Users.FirstAsync(u => u.UserLogin == login);
            if (user is null) return NotFound();
            var type = _context.UserTypes.First(ut => ut.Id == user.UserType).Name;
            return Ok(JsonConvert.SerializeObject(new UserReturnRoot()
            {
                Users = new List<UserReturn>{new()
                    {
                        Username = user.UserLogin,
                        Password = user.Password,
                        Type = type
                    }
                }
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


        #region Authentication

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthRequest model)
        {
            Console.WriteLine(nameof(this.Authenticate));
            var response = _service.AuthenticateUser(model);
            if (response is null)
                return BadRequest(new { message = "User or password in incorrect" });
            Console.WriteLine(response);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("tokenauthenticate")]
        public IActionResult AuthenticateToken([FromBody] AuthTokenRequest model)
        {
            bool verified = _service.VerifyToken(model.Token);
            string username = _service.GetClaim(model.Token, "username");
            if (verified && username.Equals(model.Username))
            {
                var user = _context.Users.First(u => u.UserLogin.Equals(username));
                return Ok(new Authenticate(user.UserLogin, user.UserType, model.Token));
            }

            return BadRequest(new { message = "Token is invalid" });
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] Token token)
        {
            string response = _service.RevokeToken(token.TokenString);
            if (response is not null) return Ok(response);
            return BadRequest();
        }
        [HttpPut("ChangePass/{login}")]
        public async Task<IActionResult> ChangePass(string login, [FromBody] Password user)
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
                if (_context.Users.First(u => u.UserLogin == user.UserLogin) is null)
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
        #endregion
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.UserLogin == id);
        }
    }
}