using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyIssue.Web.Model;
using MyIssue.Web.Services;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace MyIssue.Web.Helpers
{
    public class JwtMiddleware
    {
        private RequestDelegate _next { get; }
        private IConfiguration Configuration { get; }

        public JwtMiddleware(RequestDelegate next, IConfiguration confiugration)
        {
            _next = next;
            Configuration = confiugration;
        }

        public async Task Invoke(HttpContext context, IUserService service)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token is null) AttachToContext(context, token, service);
            await _next(context);
        }

        private void AttachToContext(HttpContext context, string token, IUserService service)
        {
            try
            {
                string username = service.GetClaim(token, "username").Result;
                bool isValid = service.ValidateToken(username, token).Result;
                if (isValid)
                {
                    context.Items["User"] = new StringContent(service.GetClaim(token, "token").Result
                    ,Encoding.UTF8, "application/json");
                }

            }
            catch
            {
            }
        }
    }
}
