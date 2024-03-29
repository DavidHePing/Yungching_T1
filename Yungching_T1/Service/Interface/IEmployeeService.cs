﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yungching_T1.Models;
using Yungching_T1.Service.Implement;
using Yungching_T1.ValueObject;

namespace Yungching_T1.Service.Interface
{
    public interface IEmployeeService
    {
        IQueryable<EmployeesInfo> GetEmployee();

        IQueryable<EmployeesInfo> GetEmployeeById(int id);

        Task<DBStateKey> UpdateEmployee(int id, Employee employee);

        Task<DBStateKey> AddNewEmployee(Employee employee);

        Task<DBStateKey> DeleteEmployee(int id);
    }
}
