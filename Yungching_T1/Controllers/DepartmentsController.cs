using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yungching_T1.Models;
using Yungching_T1.Repository;
using Yungching_T1.Repository.Implement;
using Yungching_T1.Repository.Interface;

namespace Yungching_T1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWork DB;

    public DepartmentsController(Database1Context context)
    {
        DB = new EFUnitOfWork(context);
    }

    // GET: api/Departments
    [HttpGet]
    public ActionResult<IEnumerable<Department>> GetDepartment()
    {
        return DB.GetRepository<Department>().ReadAll().ToList();
    }

    // GET: api/Departments/5
    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Department>> GetDepartment(int id)
    {
        var Department = DB.GetRepository<Department>().Read((x) => x.Id == id);

        if (Department == null)
        {
            return NotFound();
        }

        return Department;
    }

    // PUT: api/Departments/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public IActionResult PutDepartment(int id, Department Department)
    {
        if (id != Department.Id)
        {
            return BadRequest();
        }

        DB.GetRepository<Department>().Update(Department);

        try
        {
            DB.Save();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartmentExists(id))
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

    // POST: api/Departments
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public ActionResult<Department> PostDepartment(Department Department)
    {
        DB.GetRepository<Department>().Create(Department);
        DB.Save();

        return CreatedAtAction("GetDepartment", new { id = Department.Id }, Department);
    }

    // DELETE: api/Departments/5
    [HttpDelete("{id}")]
    public ActionResult<Department> DeleteDepartment(int id)
    {
        var Department = DB.GetRepository<Department>().Read((x) => x.Id == id)[0];
        if (Department == null)
        {
            return NotFound();
        }

        DB.GetRepository<Department>().Delete(Department);
        DB.Save();

        return Department;
    }

    private bool DepartmentExists(int id)
    {
        return DB.GetRepository<Department>().Read(e => e.Id == id).Any();
    }
}
}
