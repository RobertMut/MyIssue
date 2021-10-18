using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyIssue.Core.Model.Request;
using MyIssue.Web.Helpers;
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
        public async Task<IActionResult> Login([FromBody]AuthRequest model)
        {
            string response = _userService.GenerateToken(model.Username, model.Password).Result;
            if (response is null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("tokenlogin")]
        public async Task<IActionResult> TokenLogin([FromBody]TokenAuth model)
        {
            bool isValid = _userService.ValidateToken(model.Login, model.Token).Result;
            //Console.WriteLine(isValid);
            if (isValid) return Ok(new
            {
                result = true
            });
            return BadRequest(new
            {
                result = false
            });
        }
        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut([FromBody]Token model)
        {
            string token = _userService.RevokeToken(model.TokenString).Result;
            if (token.Equals("Bad request")) return BadRequest();
            return Ok(token);
        }
    }
}