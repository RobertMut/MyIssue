using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyIssue.Core.DataTransferObjects.Request;
using MyIssue.Web.Model;
using MyIssue.Web.Services;

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
    }
}