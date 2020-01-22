using BusinessObjects.Util;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Transactions;

namespace BusinessObjects.Aspect
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TransactionAttribute : Attribute
    {
        private bool _flag = true;
        public TransactionAttribute(){}
        public TransactionAttribute(bool flag)
        {
            this._flag = flag;
        }
        public bool Flag (){ 
            return this._flag; 
        }
    }
}
