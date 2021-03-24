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
        private readonly Database1Context DB;
        private readonly IEmployeeRepository empRepo;

        public EmployeesController(Database1Context db, IEmployeeRepository emp)
        {
            DB = db;
            empRepo = emp;
        }

        // GET: api/Employees
        [HttpGet]
        public ActionResult<IEnumerable<EmployeesInfo>> GetEmployee()
        {
            return empRepo.ReadAll().ToList();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<EmployeesInfo>> GetEmployee(int id)
        {
            var employee = empRepo.Read((x) => x.EmployeeID == id);

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

            empRepo.Update(employee);

            try
            {
                DB.SaveChanges();
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
            empRepo.Create(employee);
            DB.SaveChanges();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            var employee = empRepo.Read((x) => x.EmployeeID == id).ToList()
                .Select((x) => new Employee() 
                {
                    Id = x.EmployeeID,
                    Name = x.Name,
                    Age = x.Age,
                    DepartmentId = x.DepartmentID
                }).ToList()[0];
            if (employee == null)
            {
                return NotFound();
            }

            empRepo.Delete(employee);
            DB.SaveChanges();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return empRepo.Read(e => e.EmployeeID == id).Any();
        }
    }
}
