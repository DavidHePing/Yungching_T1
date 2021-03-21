using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Yungching_T1.Models;
using Yungching_T1.Repository;
using Yungching_T1.Repository.Implement;
using Yungching_T1.Repository.Interface;
using Yungching_T1.Service.Interface;
using Yungching_T1.ValueObject;

namespace Yungching_T1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IUnitOfWork DB;

        public EmployeesController(IUnitOfWork uow)
        {
            DB = uow;
        }

        // GET: api/Employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployee()
        {
            return DB.GetRepository<EmployeeRepository>().ReadAll().ToList();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Employee>> GetEmployee(int id)
        {
            var employee = DB.GetRepository<EmployeeRepository>().Read((x) => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee.ToList();
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

            DB.GetRepository<EmployeeRepository>().Update(employee);

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
            DB.GetRepository<EmployeeRepository>().Create(employee);
            DB.Save();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            var employee = DB.GetRepository<EmployeeRepository>().Read((x) => x.Id == id).ToList()[0];
            if (employee == null)
            {
                return NotFound();
            }

            DB.GetRepository<EmployeeRepository>().Delete(employee);
            DB.Save();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return DB.GetRepository<EmployeeRepository>().Read(e => e.Id == id).Any();
        }
    }
}
