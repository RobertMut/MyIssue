using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyIssue.Web.Helpers;
using MyIssue.Web.Services;

namespace MyIssue.Web.Controllers
{
    [Route("[controller]")]
    public class UserTypesController : ControllerBase
    {
        private readonly IUserTypesService _service;

        public UserTypesController(IUserTypesService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var auth = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            var result = await _service.GetUserTypes(auth);
            return Content(result, "application/json");
        }
    }
}
