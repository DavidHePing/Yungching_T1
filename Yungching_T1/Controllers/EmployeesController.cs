using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Yungching_T1.Models;
using Yungching_T1.Repository;

namespace Yungching_T1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork DB;

        public EmployeesController(Database1Context context)
        {
            DB = new EFUnitOfWork(context);
        }

        // GET: api/Employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployee()
        {
            return DB.GetRepository<Employee>().Reads().ToList();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Employee>> GetEmployee(int id)
        {
            var employee = DB.GetRepository<Employee>().Read((x) => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            DB.GetRepository<Employee>().Update(employee);

            try
            {
                DB.Save();
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            DB.GetRepository<Employee>().Create(employee);
            DB.Save();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            var employee = DB.GetRepository<Employee>().Read((x) => x.Id == id)[0];
            if (employee == null)
            {
                return NotFound();
            }

            DB.GetRepository<Employee>().Delete(employee);
            DB.Save();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return DB.GetRepository<Employee>().Read(e => e.Id == id).Any();
        }
    }
}
