using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Transactions;

namespace AopProxy.AOP.Attribute
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TransactionAttribute : AroundAttribute
    {
        public static int DefaultTimeOut = 30;

        public TransactionAttribute(TransactionScopeOption scopeOption = TransactionScopeOption.Required, IsolationLevel isolationLevel = IsolationLevel.Chaos, EnterpriseServicesInteropOption entOption = EnterpriseServicesInteropOption.None) : this(scopeOption, isolationLevel, entOption, DefaultTimeOut)
        {

        }

        public TransactionAttribute(TransactionScopeOption scopeOption, IsolationLevel isolationLevel, EnterpriseServicesInteropOption entOption, int timeOut)
        {
            this.ScopeOption = scopeOption;
            this.IsolationLevel = isolationLevel;
            this.TimeOut = TimeSpan.FromSeconds(timeOut);
        }

        public virtual TransactionScopeOption ScopeOption { get; protected set; }
        public virtual IsolationLevel IsolationLevel { get; protected set; }

        public virtual TimeSpan TimeOut { get; protected set; }

        public virtual EnterpriseServicesInteropOption EnterpriseServicesInteropOption { get; protected set; }
    }
}
