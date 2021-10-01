using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIssue.Web.Helpers;
using MyIssue.Web.Model;
using MyIssue.Web.Services;

namespace MyIssue.Web.Controllers
{
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        private IClientsService _service;
        public ClientsController(IClientsService service)
        {
            _service = service;
        }
        public async Task<ClientNameRoot> Get()
        {
            var auth = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            return await _service.GetClient(auth);
        }
    }
}
