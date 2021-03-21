﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yungching_T1.Models;
using Yungching_T1.ValueObject;

namespace Yungching_T1.Repository.Interface
{
    public interface IEmployeeRepository
    {
        void Create(Employee entity);
        IQueryable<Employee> Read(Expression<Func<Employee, bool>> predicate);
        IQueryable<Employee> ReadAll();
        void Update(Employee entity);
        void Update(Employee entity, Expression<Func<Employee, object>>[] updateProperties);
        void Delete(Employee entity);
    }
}
