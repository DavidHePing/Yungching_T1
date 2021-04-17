using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yungching_T1.Models;
using Yungching_T1.Repository;
using Yungching_T1.Repository.Interface;
using Yungching_T1.Service.Interface;
using Yungching_T1.ValueObject;

namespace Yungching_T1.Service.Implement
{
    public enum DBStateKey
    { 
        Success,
        IdIsNotExist,
        IdIsDifferent,
        Fail
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly Database1Context DB;
        private readonly IEmployeeRepository EmpRepo;


        public EmployeeService(Database1Context db, IEmployeeRepository empRepo)
        {
            DB = db;
            EmpRepo = empRepo;
        }
       
        public IQueryable<EmployeesInfo> GetEmployee()
        {
            return EmpRepo.ReadAll();
        }

        public IQueryable<EmployeesInfo> GetEmployeeById(int id)
        {
            IQueryable<EmployeesInfo> employee = EmpRepo.Read((x) => x.EmployeeID == id);

            if (employee == null)
            {
                return null;
            }

            return employee;
        }
        
        public async Task<DBStateKey> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return DBStateKey.IdIsDifferent;
            }

            if (!await EmployeeExists(id))
            {
                return DBStateKey.IdIsNotExist;
            }

            EmpRepo.Update(employee);

            try
            {
                await DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {               
                throw exception;                
            }

            return DBStateKey.Success;
        }
        
        public async Task<DBStateKey> AddNewEmployee(Employee employee)
        {
            EmpRepo.Create(employee);
            await DB.SaveChangesAsync();

            return DBStateKey.Success;
        }
      
        public async Task<DBStateKey> DeleteEmployee(int id)
        {
            IEnumerable<EmployeesInfo> empInfo = await EmpRepo.Read((x) => x.EmployeeID == id).ToListAsync();
            Employee employee = empInfo
                .Select((x) => new Employee()
                {
                    Id = x.EmployeeID,
                    Name = x.Name,
                    Age = x.Age,
                    DepartmentId = x.DepartmentID
                }).ToList()[0];

            if (employee == null)
            {
                return DBStateKey.IdIsNotExist;
            }

            EmpRepo.Delete(employee);
            DB.SaveChanges();

            return DBStateKey.Success;
        }

        private async Task<bool> EmployeeExists(int id)
        {
            return await EmpRepo.Read(e => e.EmployeeID == id).AnyAsync();
        }
    }
}
