using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Filter;
using MyIssue.API.Infrastructure;
using MyIssue.API.Services;
using MyIssue.Core.DataTransferObjects.Request;
using MyIssue.Core.DataTransferObjects.Return;

namespace MyIssue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MyIssueContext _context;
        private readonly IUserService _userService;

        public AuthController(MyIssueContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthRequest model)
        {
            Console.WriteLine(nameof(this.Authenticate));
            var response = _userService.AuthenticateUser(model);
            if (response is null)
                return BadRequest(new { message = "User or password in incorrect" });
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
                return Ok(new Authenticate(user.UserLogin, user.UserType, model.Token));
            }

            return BadRequest(new { message = "Token is invalid" });
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] Token token)
        {
            string response = _userService.RevokeToken(token.TokenString);
            if (response is not null) return Ok(response);
            return BadRequest();
        }

        // [Authorize]
        // [HttpPost("checktoken")]
        // public IActionResult CheckToken([FromBody] Token token)
        // {
        //     var users = _userService.GetAll();
        //     return Ok(users);
        // }
        [HttpPut("Put/{login}")]
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
    }
}
