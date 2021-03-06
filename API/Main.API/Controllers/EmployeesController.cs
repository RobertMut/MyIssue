using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Main.API.Infrastructure;
using MyIssue.Main.API.Model;
using Newtonsoft.Json;

namespace MyIssue.Main.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly MyIssueContext _context;

        public EmployeesController(MyIssueContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet("GetFull/")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetFullEmployees()
        {

            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("GetFull/{id}")]
        public async Task<ActionResult<Employee>> GetFullEmployee(string id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }
        [HttpGet]
        public async Task<ActionResult<UserReturnRoot>> GetEmployees()
        {
            List<EmployeeReturn> employeeList = new List<EmployeeReturn>();
            var employees = await _context.Employees.ToListAsync();
            employees.ForEach(e => employeeList.Add(new EmployeeReturn
            {
                Login = e.EmployeeLogin,
                Name = e.EmployeeName,
                Surname = e.EmployeeSurname,
                No = e.EmployeeNo,
                Position = _context.Positions.FirstOrDefault(p => p.PositionId==e.EmployeePosition).PositionName
            }));
            return Ok(JsonConvert.SerializeObject(new EmployeeReturnRoot()
            {
                Employees = employeeList
            }));
        }

        // GET: api/Users/{login}
        [HttpGet("{login}")]
        public async Task<ActionResult<User>> GetEmployee(string login)
        {
            List<EmployeeReturn> employeeList = new List<EmployeeReturn>();
            var employees = await _context.Employees.FirstOrDefaultAsync(l => l.EmployeeLogin == login);
            if (employees == null) return NotFound();
            var employee = new EmployeeReturn
            {
                Login = employees.EmployeeLogin,
                Name = employees.EmployeeName,
                Surname = employees.EmployeeSurname,
                No = employees.EmployeeNo,
                Position = _context.Positions.FirstOrDefault(p => p.PositionId == employees.EmployeePosition).PositionName
            };
            return Ok(JsonConvert.SerializeObject(new EmployeeReturnRoot()
            {
                Employees = new List<EmployeeReturn>(){employee}
            }));
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(string id, Employee employee)
        {
            if (id != employee.EmployeeLogin)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeReturn employee)
        {
            _context.Employees.Add(new Employee
            {
                EmployeeLogin = employee.Login,
                EmployeeName = employee.Name,
                EmployeeSurname = employee.Surname,
                EmployeeNo = employee.No,
                EmployeePosition = _context.Positions.Where(e => e.PositionName == employee.Position).First().PositionId,
            });
            var user = _context.Users.FirstOrDefault(u => u.UserLogin == employee.Login);
            try
            {
                if (user is not null)
                    _context.EmployeeUser.Add(new EmployeeUser
                    {
                        UserLogin = employee.Login,
                        EmployeeLogin = employee.Login,
                    });
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.Login))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployee", new { EmployeeLogin = employee.Login }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.EmployeeLogin == id);
        }
    }
}
