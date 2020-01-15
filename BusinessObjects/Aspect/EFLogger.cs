using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BusinessObjects.Aspect
{
    public class EFLogger:ILogger
    {
        private readonly string categoryName;

        public EFLogger(string categoryName) => this.categoryName = categoryName;

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

            Debug.WriteLine($"时间:{DateTime.Now.ToString("o")} 日志级别: {logLevel} {eventId.Id} 产生的类{this.categoryName}");
            //DbCommandLogData data = state as DbCommandLogData;
            //Debug.WriteLine($"SQL语句:{data.CommandText},\n 执行消耗时间:{data.ElapsedMilliseconds}");

        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }

    public class NullLogger : ILogger
    {
        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        { }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }

    public class BOLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            // NOTE: 这里要注意,这是 EF Core 1.1的使用方式,如果你用的 EF Core 1.0, 就需把IRelationalCommandBuilderFactory替换成下面的类
            //       Microsoft.EntityFrameworkCore.Storage.Internal.RelationalCommandBuilderFactory

            if (categoryName == typeof(IRelationalCommandBuilderFactory).FullName)
            {
                return new EFLogger(categoryName);
            }

            return new NullLogger();
        }
        public void Dispose()
        { }
    }
}
