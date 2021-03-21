using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Yungching_T1.Models;
using Yungching_T1.Repository.Interface;
using Yungching_T1.ValueObject;

namespace Yungching_T1.Repository.Implement
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly Database1Context Context;
        public EmployeeRepository(Database1Context context)
        {
            Context = context;
        }

        /// <summary>
        /// 新增一筆資料到資料庫。
        /// </summary>
        /// <param name="entity">要新增到資料的庫的Entity</param>
        public void Create(Employee entity)
        {
            Context.Set<Employee>().Add(entity);
        }

        /// <summary>
        /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得第一筆符合條件的內容。</returns>
        public IQueryable<Employee> Read(Expression<Func<Employee, bool>> predicate)
        {
            return Context.Set<Employee>().Where(predicate).AsQueryable();
        }

        /// <summary>
        /// 取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <returns>Entity全部筆數的IQueryable。</returns>
        public IQueryable<Employee> ReadAll()
        {
            return Context.Set<Employee>().AsQueryable();
        }

        /// <summary>
        /// 更新一筆Entity內容。
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        public void Update(Employee entity)
        {
            Context.Entry<Employee>(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 更新一筆Entity的內容。只更新有指定的Property。
        /// </summary>
        /// <param name="entity">要更新的內容。</param>
        /// <param name="updateProperties">需要更新的欄位。</param>
        public void Update(Employee entity, Expression<Func<Employee, object>>[] updateProperties)
        {
            Context.Entry<Employee>(entity).State = EntityState.Unchanged;

            if (updateProperties != null)
            {
                foreach (var property in updateProperties)
                {
                    Context.Entry<Employee>(entity).Property(property).IsModified = true;
                }
            }
        }

        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="entity">要被刪除的Entity。</param>
        public void Delete(Employee entity)
        {
            Context.Entry<Employee>(entity).State = EntityState.Deleted;
        }
    }
}
