using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIssue.API.Infrastructure;
using MyIssue.API.Model;
using MyIssue.API.Model.Return;
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
        public async Task<ActionResult<ClientNameRoot>> GetClients()
        {
            List<ClientNameReturn> clientList = new List<ClientNameReturn>();
            var clients = await _context.Clients.ToListAsync();
            clients.ForEach(e => clientList.Add(new ClientNameReturn
            {
                CompanyName = e.ClientName
            }));
            return Ok(JsonConvert.SerializeObject(new ClientNameRoot()
            {
                Clients = clientList
            }));
        }

        // GET: api/Clients/5
        [HttpGet("Full/{id}")]
        public async Task<ActionResult<Client>> GetClient(decimal id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
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

            return client;
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
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
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
