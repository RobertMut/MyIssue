using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Core.Model.Return;
using MyIssue.Web.Helpers;
using MyIssue.Web.Model;
using MyIssue.Web.Services;

namespace MyIssue.Web.Controllers
{
    [Route("[controller]")]
    public class TaskTypesController : ControllerBase
    {
        private readonly ITaskTypesService _service;

        public TaskTypesController(ITaskTypesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<TaskTypeReturnRoot> Get()
        {
            var token = this.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //Console.WriteLine("TOKEN   " + token);
            var auth = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            return await _service.GetTaskTypes(auth);
        }
    }
}
