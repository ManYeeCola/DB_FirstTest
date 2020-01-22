using BusinessObjects.Aspect;
using BusinessObjects.Util;
using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Reflection;

namespace BusinessObjects.Interceptor
{
    [LoggingAspect(AspectPriority = 1)]
    public class TransactionInterceptor:Castle.DynamicProxy.IInterceptor
    {
        private AppDbContext _db;
        public TransactionInterceptor(){}
        public TransactionInterceptor(AppDbContext db)
        {
            this._db = db;
        }
        // 是否开发模式
        private bool isDev = false;
        public void Intercept(IInvocation invocation)
        {
            if (!isDev)
            {
                MethodInfo methodInfo = invocation.MethodInvocationTarget;
                if (methodInfo == null)
                {
                    methodInfo = invocation.Method;
                }

                TransactionAttribute transaction = methodInfo.GetCustomAttributes<TransactionAttribute>(true)
                 .FirstOrDefault();
                if (null != transaction && transaction.Flag())
                {
                    using (var tran = this.BeginTransaction())
                    {
                        try
                        {
                                invocation.Proceed();
                                tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            this.RollBack();
                            throw ex;
                        }
                        finally
                        {
                            //Do somthing……
                        }
                    }
                }
                else
                    invocation.Proceed();
            }
            else
                invocation.Proceed();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return this._db.Database.BeginTransaction();
        }
        public void RollBack()
        {
            this._db.Database.RollbackTransaction();
        }
    }
}
