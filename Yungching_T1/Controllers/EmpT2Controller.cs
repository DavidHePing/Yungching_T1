using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yungching_T1.Models;
using Yungching_T1.Repository.Interface;

namespace Yungching_T1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpT2Controller : ControllerBase
    {
        private readonly Database1Context DB;
        private readonly IEmployeeRepository empRepo;
        private readonly IDepartmentRepository departmentRepo;

        public EmpT2Controller(Database1Context db, IEmployeeRepository emp, IDepartmentRepository dep)
        {
            DB = db;
            empRepo = emp;
            departmentRepo = dep;
        }

        [HttpGet]
        public void Test()
        {
            empRepo.Create(new Employee() {Name = "testEmp", DepartmentId = 1 });
            departmentRepo.Create(new Department() { Name = "testEmp"});
            DB.SaveChanges();
        }
    }
}
