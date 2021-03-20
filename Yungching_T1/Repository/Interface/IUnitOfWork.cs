using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yungching_T1.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 儲存所有異動。
        /// </summary>
        void Save();

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// </summary>
        /// <typeparam name="T">此Context裡面的Entity Type</typeparam>
        /// <returns>Entity的Repository</returns>
        IRepository<T> GetRepository<T>() where T : class;
    }
}