using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Infrastructure;
using Newtonsoft.Json;

namespace MyIssue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserTypesController : ControllerBase
    {
        private readonly MyIssueContext _context;

        public UserTypesController(MyIssueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserTypes()
        {
            var userTypes = await _context.UserTypes.ToListAsync();
            if (userTypes is null) return NoContent();
            return Ok(JsonConvert.SerializeObject(userTypes));
        }
    }
}
