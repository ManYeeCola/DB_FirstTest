using BusinessObjects.Aspect;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Transactions;

namespace BusinessObjects.Interceptor
{
    public class TransactionInterceptor:Castle.DynamicProxy.IInterceptor
    {
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

                TransactionAttribute transaction =
                    methodInfo.GetCustomAttributes<TransactionAttribute>(true).FirstOrDefault();
                if (transaction != null)
                {
                    try
                    {
                        using (var tran = transaction.BeginTransaction())
                        {
                            //实现事务性工作
                            invocation.Proceed();
                            tran.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        // 记录异常
                        transaction.RollBack();
                        throw ex;
                    }
                    finally
                    {
                        transaction.RollBack();
                        //Dosomething
                    }
                }
                else
                {
                    // 没有事务时直接执行方法
                    invocation.Proceed();
                }
            }
            else
            {
                // 开发模式直接跳过拦截
                invocation.Proceed();
            }
        }
    }
}
