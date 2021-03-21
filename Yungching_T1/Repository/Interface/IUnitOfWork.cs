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
        int Save();

        /// <summary>
        /// 取得某一個Entity的Repository。
        /// 如果沒有取過，會initialise一個
        /// 如果有就取得之前initialise的那個。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetRepository<T>() where T : class;
    }
}