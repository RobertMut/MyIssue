using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Infrastructure;
using Newtonsoft.Json;

namespace MyIssue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PositionsController : ControllerBase
    {
        private readonly MyIssueContext _context;

        public PositionsController(MyIssueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPositions()
        {
            var positions = await _context.Positions.ToListAsync();
            if (positions is null) return NotFound();
            return Ok( JsonConvert.SerializeObject(positions));
        }
    }
}
