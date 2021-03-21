using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yungching_T1.Models;
using Yungching_T1.Repository.Interface;

namespace Yungching_T1.Repository.Implement
{
    public class Database1UnitOfWork : IUnitOfWork
    {
        private readonly Database1Context _context;
        private Hashtable _repositories;
        private bool _disposed;

        /// <summary>
        /// 設定此Unit of work(UOF)的Context。
        /// </summary>
        /// <param name="context">設定UOF的context</param>
        public Database1UnitOfWork(Database1Context context)
        {
            _context = context;
            _repositories = new Hashtable
            {
                { typeof(EmployeeRepository), new EmployeeRepository(context) },
                { typeof(DepartmentRepository), new DepartmentRepository(context) }
            };
        }

        /// <summary>
        /// 儲存所有異動。
        /// </summary>
        public int Save()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 清除此Class的資源。
        /// </summary>
        /// <param name="disposing">是否在清理中？</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <typeparam name="T">此Context裡面的Entity Type</typeparam>
        /// <returns>Entity的Repository</returns>
        public T GetRepository<T>() where T : class
        {           
            return (T)_repositories[typeof(T)];
        }
    }
}