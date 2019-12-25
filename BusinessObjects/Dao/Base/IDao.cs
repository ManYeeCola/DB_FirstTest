using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BusinessObjects.Dao
{
    public interface IDao<T> where T :class
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
        /// <param name="Id">主键ID</param>
        /// <returns>对象</returns>
        T GetById(long Id);
        /// <summary>
        /// 根据ID获取一个对象
        /// </summary>
        /// <param name="Id">主键ID</param>
        /// <returns>对象</returns>
        T GetById(string Id);
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
        /// 关闭代理
        /// </summary>
        void CloseProxy();
        /// <summary>
        /// 打开代理
        /// </summary>
        void OpenProxy();
        /// <summary>
        /// 是否有指定条件的元素
        /// </summary>
        /// <param name="where">条件(lambda表达式)</param>
        /// <returns></returns>
        bool IsHasValue(Expression<Func<T, bool>> where);
    }
}
