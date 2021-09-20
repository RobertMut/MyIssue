using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MyIssue.Core.String;
using MyIssue.Web.Model;
using MyIssue.Web.Services;

namespace MyIssue.Web.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]

    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                IServiceProvider services = context.HttpContext.RequestServices;
                IUserService service = services.GetService<IUserService>();
                var headers = context.HttpContext.Request.Headers;
                foreach (var keyValuePair in headers)
                {
                    Console.WriteLine(keyValuePair.Key + " " + keyValuePair.Value);
                }
                string[] auth = StringStatic.CommandSplitter(
                    headers.First(c => c.Key.Equals("Authorization")).Value, " "
                );
                if (auth.Length.Equals(0)) throw new AuthenticationException("Authentication Error");
                Console.WriteLine($"Validating {auth[0]}...");
                bool isValid = service.ValidateToken(auth[0], auth[1]).Result;
                if (!isValid) throw new AuthenticationException("Authentication error");
            }
            catch (AuthenticationException ae)
            {
                context.Result = new JsonResult(new
                {
                    message = ae.Message
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
