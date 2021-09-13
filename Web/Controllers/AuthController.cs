using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyIssue.Web.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        public AuthController()
        {
        }


        public async Task<IActionResult> LogIn(string url)
        {
            var user = User as ClaimsPrincipal;
            var token = await HttpContext.GetTokenAsync("access_token");
            _logger.LogInformation("{@User} logged in", user);
            if (token is not null)
            {
                ViewData["access_token"] = token;
            }

            return RedirectToAction(nameof(TasksController.Index), "Tasks");
        }
        public Task
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            var url = Url.Action(nameof(TasksController.), "Tasks");
            return new SignInResult(OpenIdConnectDefaults.AuthenticationScheme,
                new AuthenticationProperties({RedirectUri = url}));
        }
    }
}