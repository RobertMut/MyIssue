using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyIssue.Core.Model.Return;
using MyIssue.Web.Helpers;
using MyIssue.Web.Services;

namespace MyIssue.Web.Controllers
{
    [Route("[controller]")]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionsService _service;

        public PositionsController(IPositionsService _service)
        {
            this._service = _service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var auth = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            var result = await _service.GetEmployeePositons(auth);
            return Content(result, "application/json");
        }
    }
}
