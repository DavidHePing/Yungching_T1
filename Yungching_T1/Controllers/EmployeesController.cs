using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yungching_T1.Models;
using Yungching_T1.Service.Implement;
using Yungching_T1.Service.Interface;
using Yungching_T1.ValueObject;

namespace Yungching_T1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService EmpService;

        private readonly Dictionary<DBStateKey, string> DBState = new Dictionary<DBStateKey, string>();

        public EmployeesController(IEmployeeService employeeService)
        {
            EmpService = employeeService;

            DBState.Add(DBStateKey.Success, "Update Success!!");
            DBState.Add(DBStateKey.IdIsNotExist, "Employee's Id is not exist");
            DBState.Add(DBStateKey.IdIsDifferent, "Id and Employee's Id is different");
            DBState.Add((DBStateKey.Fail), "Update Failure!!");
        }

        // GET: api/Employees
        [HttpGet]
        public async ValueTask<IEnumerable<EmployeesInfo>> GetEmployee()
        {
            return await EmpService.GetEmployee().ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<EmployeesInfo>>> GetEmployee(int id)
        {
            IQueryable<EmployeesInfo> employeeQuery = EmpService.GetEmployeeById(id);
            List<EmployeesInfo> employees = await employeeQuery.ToListAsync();

            if (!employees.Any())
            {
                return NotFound();
            }

            return employees;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeAsync(int id, Employee employee)
        {
            DBStateKey updateState = await EmpService.UpdateEmployee(id, employee);

            if (updateState != DBStateKey.Success)
                return StatusCode(500, DBState[updateState]);

            return Ok(DBState[updateState]);
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostEmployee(Employee employee)
        {
            DBStateKey updateState = await EmpService.AddNewEmployee(employee);

            if (updateState != DBStateKey.Success)
                return StatusCode(500, DBState[updateState]);

            return Ok(DBState[updateState]);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            DBStateKey updateState = await EmpService.DeleteEmployee(id);

            if (updateState != DBStateKey.Success)
                return StatusCode(500, DBState[updateState]);

            return Ok(DBState[updateState]);
        }
    }
}
