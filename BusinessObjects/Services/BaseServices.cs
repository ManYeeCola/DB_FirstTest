using BusinessObjects.Dao;
using BusinessObjects.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessObjects.Services
{
    public interface IServices<T>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Add(T entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Update(T entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Delete(T entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        void Delete(Expression<Func<T, bool>> where);
        /// <summary>
        /// 根据ID获取一个对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>对象</returns>
        T GetById(int id);
        /// <summary>
        /// 根据ID获取一个对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>对象</returns>
        T GetById(string id);
        /// <summary>
        /// 根据条件获取一个对象
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns>对象</returns>
        T Get(Expression<Func<T, bool>> where);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>所有数据</returns>
        List<T> GetAll();
        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns>数据</returns>
        List<T> GetMany(Expression<Func<T, bool>> where);
        /// <summary>
        /// 根据条件获取记录数
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> where);
        /// <summary>
        /// 关闭代理
        /// </summary>
        void CloseProxy();
        /// <summary>
        /// 打开代理
        /// </summary>
        void OpenProxy();
        /// <summary>
        /// 回滚操作
        /// </summary>
        void RollBack();
        /// <summary>
        /// 是否有指定条件的元素
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns></returns>
        bool IsHasValue(Expression<Func<T, bool>> where);
        int SaveChanges();
        void Dispose(bool disposing);
    }




    public abstract class BaseServices<T> :IServices<T> where T:class
    {
        protected AppDbContext _db;
        private IDao<T> _baseDao;
        private bool _isDisposed;

        public BaseServices(AppDbContext db, IDao<T> baseDao)
        {
            _db = db;
            _baseDao = baseDao;
            Debug.WriteLine("Services--" + _db.ContextId);
        }

        public void Add(T entity)
        {
            _baseDao.Add(entity);
            this.SaveChanges();
        }
        public void Update(T entity)
        {
            _baseDao.Update(entity);
            this.SaveChanges();
        }
        public void Delete(T entity)
        {
            _baseDao.Delete(entity);
            this.SaveChanges();
        }
        public void Delete(Expression<Func<T, bool>> where)
        {
            _baseDao.Delete(where);
            this.SaveChanges();
        }
        public T Get(Expression<Func<T, bool>> where)
        {
            return _baseDao.Get(where);
        }

        public List<T> GetAll()
        {
            return _baseDao.GetAll().ToList();
        }

        public T GetById(int id)
        {
            return _baseDao.GetById(id);
        }

        public T GetById(string id)
        {
            return _baseDao.GetById(id);
        }

        public int GetCount(Expression<Func<T, bool>> where)
        {
            return _baseDao.GetCount(where);
        }

        public List<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _baseDao.GetMany(where).ToList();
        }

        public bool IsHasValue(Expression<Func<T, bool>> where)
        {
            return _baseDao.IsHasValue(where);
        }
        public int SaveChanges()
        {
            return _db.SaveChanges();
        }
        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _db.SaveChangesAsync(cancellationToken);
        }
        public void OpenProxy()
        {
            _db.Database.BeginTransaction();
        }
        public void CloseProxy()
        {
            _db.Database.CommitTransaction();
        }
        public void RollBack()
        {
            _db.Database.RollbackTransaction();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
            _isDisposed = true;
        }
    }
}
