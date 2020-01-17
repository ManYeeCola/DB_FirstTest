using BusinessObjects.Util;
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessObjects.Dao
{
    public interface IDao<T> : IDependency where T : class
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
        IQueryable<T> GetAll();
        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns>数据</returns>
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        /// <summary>
        /// 根据条件获取记录数
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> where);
        /// <summary>
        /// 是否有指定条件的元素
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns></returns>
        bool IsHasValue(Expression<Func<T, bool>> where);
    }

    public abstract class BaseDao<T>:IDao<T> where T:class
    {
        protected AppDbContext _db;//数据库上下文

        public BaseDao(AppDbContext db)
        {
            _db = db;
            Debug.WriteLine("Dao--" + _db.ContextId);
        }

        public virtual void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }
        
        public virtual void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }
        
        public virtual void Delete(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            var dataList = _db.Set<T>().Where(where).AsEnumerable();
            _db.Set<T>().RemoveRange(dataList);
        }

        public virtual T Get(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return _db.Set<T>().FirstOrDefault(where);
        }

        public virtual System.Linq.IQueryable<T> GetAll()
        {
            return _db.Set<T>();
        }

        public virtual T GetById(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public virtual T GetById(string id)
        {
            return _db.Set<T>().Find(id);
        }

        public virtual int GetCount(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return _db.Set<T>().Count(where);
        }

        public virtual System.Linq.IQueryable<T> GetMany(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return _db.Set<T>().Where(where);
        }

        public virtual bool IsHasValue(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return _db.Set<T>().Any(where);
        }
        
        public virtual void Update(T entity)
        {
            _db.Set<T>().Attach(entity);
            _db.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
