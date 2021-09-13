using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyIssue.API.Services;

namespace MyIssue.API.Helpers
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
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration.GetValue<string>("Token:Issuer"),
                    ValidAudience = Configuration.GetValue<string>("Token:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(
                            Configuration.GetValue<string>("Token:Secret")
                        ))
                }, out SecurityToken validated);
                var jwtToken = (JwtSecurityToken) validated;
                var userLogin = jwtToken.Claims.First(x => x.Type == "username").Value;
                context.Items["User"] = service.GetByLogin(userLogin);
            }
            catch
            {
            }
        }
    }
}
