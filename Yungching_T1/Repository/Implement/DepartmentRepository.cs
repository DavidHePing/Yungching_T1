using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yungching_T1.Models;
using Yungching_T1.Repository.Interface;

namespace Yungching_T1.Repository.Implement
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly Database1Context Context;
        public DepartmentRepository(Database1Context context)
        {
            Context = context;
        }

        /// <summary>
        /// 新增一筆資料到資料庫。
        /// </summary>
        /// <param name="entity">要新增到資料的庫的Entity</param>
        public void Create(Department entity)
        {
            Context.Set<Department>().Add(entity);
        }

        /// <summary>
        /// 取得第一筆符合條件的內容。如果符合條件有多筆，也只取得第一筆。
        /// </summary>
        /// <param name="predicate">要取得的Where條件。</param>
        /// <returns>取得第一筆符合條件的內容。</returns>
        public IQueryable<Department> Read(Expression<Func<Department, bool>> predicate)
        {
            return Context.Set<Department>().Where(predicate).AsQueryable();
        }

        /// <summary>
        /// 取得Entity全部筆數的IQueryable。
        /// </summary>
        /// <returns>Entity全部筆數的IQueryable。</returns>
        public IQueryable<Department> ReadAll()
        {
            return Context.Set<Department>().AsQueryable();
        }

        /// <summary>
        /// 更新一筆Entity內容。
        /// </summary>
        /// <param name="entity">要更新的內容</param>
        public void Update(Department entity)
        {
            Context.Entry<Department>(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 更新一筆Entity的內容。只更新有指定的Property。
        /// </summary>
        /// <param name="entity">要更新的內容。</param>
        /// <param name="updateProperties">需要更新的欄位。</param>
        public void Update(Department entity, Expression<Func<Department, object>>[] updateProperties)
        {
            Context.Entry<Department>(entity).State = EntityState.Unchanged;

            if (updateProperties != null)
            {
                foreach (var property in updateProperties)
                {
                    Context.Entry<Department>(entity).Property(property).IsModified = true;
                }
            }
        }

        /// <summary>
        /// 刪除一筆資料內容。
        /// </summary>
        /// <param name="entity">要被刪除的Entity。</param>
        public void Delete(Department entity)
        {
            Context.Entry<Department>(entity).State = EntityState.Deleted;
        }
    }
}
