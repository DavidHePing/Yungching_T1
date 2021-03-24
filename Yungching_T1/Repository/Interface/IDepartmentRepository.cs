using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yungching_T1.Models;

namespace Yungching_T1.Repository.Interface
{
    public interface IDepartmentRepository
    {
        void Create(Department entity);
        IQueryable<Department> Read(Expression<Func<Department, bool>> predicate);
        IQueryable<Department> ReadAll();
        void Update(Department entity);
        void Update(Department entity, Expression<Func<Department, object>>[] updateProperties);
        void Delete(Department entity);
    }
}
