using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Web.Helpers;
using MyIssue.Web.Model;

namespace MyIssue.Web.Controllers
{
    [Route("[controller]")]
    public class TaskTypesController : ControllerBase
    {
        [HttpGet]
        public async Task<TaskTypeReturnRoot> Get()
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            Console.WriteLine("TOKEN   " + token);
            var auth = TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            //return service gettasktype
        }
    }
}
