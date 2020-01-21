using BusinessObjects.Util;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Transactions;

namespace BusinessObjects.Aspect
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionAttribute : Attribute
    {
        private AppDbContext _db;
        public TransactionAttribute(){}
        public TransactionAttribute(AppDbContext db)
        {
            this._db = db;
        }
        public IDbContextTransaction BeginTransaction()
        {
            Debug.WriteLine(this._db.ContextId + "-----------------");
            return this._db.Database.BeginTransaction();
        }
        public void RollBack()
        {
            this._db.Database.RollbackTransaction();
        }
    }
}
