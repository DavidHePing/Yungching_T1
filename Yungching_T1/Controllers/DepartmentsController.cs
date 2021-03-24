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
        private readonly Database1Context DB;
        private readonly IDepartmentRepository departmentRepo;

        public DepartmentsController(Database1Context db, IDepartmentRepository dep)
        {
            DB = db;
            departmentRepo = dep;
        }

    // GET: api/Departments
    [HttpGet]
    public ActionResult<IEnumerable<Department>> GetDepartment()
    {
        return departmentRepo.ReadAll().ToList();
    }

    // GET: api/Departments/5
    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Department>> GetDepartment(int id)
    {
        var Department = departmentRepo.Read((x) => x.Id == id);

        if (Department == null)
        {
            return NotFound();
        }

        return Department.ToList();
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

        departmentRepo.Update(Department);

        try
        {
            DB.SaveChanges();
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
        departmentRepo.Create(Department);
        DB.SaveChanges();

        return CreatedAtAction("GetDepartment", new { id = Department.Id }, Department);
    }

    // DELETE: api/Departments/5
    [HttpDelete("{id}")]
    public ActionResult<Department> DeleteDepartment(int id)
    {
        var Department = departmentRepo.Read((x) => x.Id == id).ToList()[0];
        if (Department == null)
        {
            return NotFound();
        }

        departmentRepo.Delete(Department);
        DB.SaveChanges();

        return Department;
    }

    private bool DepartmentExists(int id)
    {
        return departmentRepo.Read(e => e.Id == id).Any();
    }
}
}
