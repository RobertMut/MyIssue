using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MyIssue.API.Infrastructure;

namespace MyIssue.API.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class BasicAuthAttribute : AuthorizationFilterAttribute, IAuthorizationFilter
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var context =
                actionContext.ActionDescriptor.Configuration.Services.GetService(typeof(MyIssueContext)) as
                    MyIssueContext;
            if (actionContext.Request.Headers.Authorization == null)
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string[] userPass = decodedToken.Split(':');
                string login = userPass[0];
                string pass = userPass[1];
                var user = context.Users.First(u => u.UserLogin == login && u.Password == pass);
                if (user is not null)
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(login), null);
                else
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

            }
        }
    }
}
