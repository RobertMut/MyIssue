using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Infrastructure;
using MyIssue.API.Model;
using MyIssue.Core.DataTransferObjects.Return;
using Newtonsoft.Json;

namespace MyIssue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly MyIssueContext _context;

        public ClientsController(MyIssueContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet("Full")]
        public async Task<ActionResult<IEnumerable<Client>>> GetFullClients()
        {
            return await _context.Clients.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<ClientReturnRoot>> GetClients()
        {
            List<ClientReturn> clientList = new List<ClientReturn>();
            var clients = await _context.Clients.ToListAsync();
            clients.ForEach(e => clientList.Add(new ClientReturn
            {
                Id = Convert.ToInt32(e.ClientId),
                Name = e.ClientName,
                Country = e.ClientCountry,
                No = e.ClientFlatNo,
                Street = e.ClientStreet,
                StreetNo = e.ClientStreetNo,
                FlatNo = e.ClientFlatNo,
                Description = e.ClientDesc
            }));
            return Content(JsonConvert.SerializeObject(new ClientReturnRoot()
            {
                Clients = clientList
            }),"application/json");
        }

        // GET: api/Clients/5
        [HttpGet("Full/{id}")]
        public async Task<ActionResult<string>> GetClient(decimal id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            var serialized = JsonConvert.SerializeObject(client);
            return Content(serialized, "application/json");
        }
        //GET: api/Clients/{Name}
        [HttpGet("Full/{name}")]
        public async Task<ActionResult<Client>> GetClient(string name)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientName.Equals(name));
            if (client is null)
            {
                return NotFound();
            }

            var serialized = JsonConvert.SerializeObject(client);
            return Content(serialized, "application/json");
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(decimal id, Client client)
        {
            if (id != client.ClientId)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient([FromBody]ClientReturn client)
        {
            _context.Clients.Add(new Client
            {
                ClientName = client.Name,
                ClientCountry = client.Country,
                ClientNo = client.No,
                ClientStreet = client.Street,
                ClientStreetNo = client.StreetNo,
                ClientFlatNo = client.FlatNo,
                ClientDesc = client.Description
            });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_context.Clients.First(c => c.ClientNo == client.No) != null)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { name = client.Name }, client.Name);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(decimal id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(decimal id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
    }
}
