using Fullstack.API.Data;
using Fullstack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Fullstack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullstackDBContext _fullstackDbContext;

        public EmployeesController(FullstackDBContext fullstackDbContext)
        {
            _fullstackDbContext = fullstackDbContext;
        }
        [HttpGet]
        public async Task <IActionResult> GetAllEmployees()
        {
            var employees = await _fullstackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _fullstackDbContext.Employees.AddAsync(employeeRequest);
            await _fullstackDbContext.SaveChangesAsync();

            return Ok(employeeRequest); 
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = 
            await _fullstackDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee == null) 
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRewquest)
        {
            var employee = await _fullstackDbContext.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }

            employee.FirstName = updateEmployeeRewquest.FirstName;
            employee.LastName = updateEmployeeRewquest.LastName;
            employee.Email = updateEmployeeRewquest.Email;
            employee.Phone = updateEmployeeRewquest.Phone;
            employee.City = updateEmployeeRewquest.City;
            employee.Salary = updateEmployeeRewquest.Salary;
            employee.Department = updateEmployeeRewquest.Department;

            await _fullstackDbContext.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _fullstackDbContext.Employees.FindAsync(id);
             if(employee == null)
            {
                return NotFound();
            }

            _fullstackDbContext.Employees.Remove(employee);
            await _fullstackDbContext.SaveChangesAsync();
            return Ok(employee);
        }

    }
}
