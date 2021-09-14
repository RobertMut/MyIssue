using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyIssue.Web.Model;
using MyIssue.Web.Services;
using Task = System.Threading.Tasks.Task;

namespace MyIssue.Web.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest model)
        {
           string response = await _userService.GenerateToken(model.Login, model.Password);
           if (response is null)
           {
               return BadRequest();
           }

           return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("tokenlogin")]
        public async Task<IActionResult> TokenLogin(TokenAuth model)
        {
            bool isValid = await _userService.ValidateToken(model.Login, model.Token);
            if (isValid) return Ok(true);
            return BadRequest(false);
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut(Token model)
        {
            string token = await _userService.RevokeToken(model.TokenString);
            if (token.Equals("Bad request")) return BadRequest();
            return Ok(token);
        }
    }
}