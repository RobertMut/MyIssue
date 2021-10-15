using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyIssue.Core.Model.Return;
using MyIssue.Web.Helpers;
using MyIssue.Web.Services;

namespace MyIssue.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        private IClientsService _service;
        public ClientsController(IClientsService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ClientReturnRoot> Get()
        {
            var auth = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            return await _service.GetClient(auth);
        }

        [HttpPost("new")]
        public async Task<IActionResult> Post([FromBody] ClientReturn client)
        {
            var auth = await TokenHelper.GetTokenFromHeader(this.HttpContext.Request.Headers);
            return Ok(await _service.PostClient(client, auth));
        }
    }
}
