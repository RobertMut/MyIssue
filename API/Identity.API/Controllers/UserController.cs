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
            try
            {

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


     
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.UserLogin == id);
        }
    }
}