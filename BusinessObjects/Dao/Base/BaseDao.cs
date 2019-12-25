using BusinessObjects.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects.Dao
{
    public abstract class BaseDao<T> where T:class
    {
        protected AppDbContext db;//数据库上下文

        public BaseDao(AppDbContext _db)
        {
            db = _db;
        }

        public virtual void Save()
        {
            db.SaveChanges();
        }

        public virtual void Add(T entity)
        {
            db.Set<T>().Add(entity);
        }

        public virtual void CloseProxy()
        {
            db.Database.CommitTransaction();
        }

        public virtual void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
        }

        public virtual void Delete(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            var dataList = db.Set<T>().Where(where).AsEnumerable();
            db.Set<T>().RemoveRange(dataList);
        }

        public virtual T Get(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return db.Set<T>().FirstOrDefault(where);
        }

        public virtual System.Linq.IQueryable<T> GetAll()
        {
            return db.Set<T>();
        }

        public virtual T GetById(long Id)
        {
            return db.Set<T>().Find(Id);
        }

        public virtual T GetById(string Id)
        {
            return db.Set<T>().Find(Id);
        }

        public virtual int GetCount(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Count(where);
        }

        public virtual System.Linq.IQueryable<T> GetMany(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Where(where);
        }

        public virtual bool IsHasValue(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            return db.Set<T>().Any(where);
        }

        public virtual void OpenProxy()
        {
            db.Database.BeginTransaction();
        }

        public virtual void Update(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
