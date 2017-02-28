using AopProxy.AOP.Advice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopProxy.AOP;
using AopProxy.AOP.Attribute;
using System.Transactions;
using System.Data.Common;

namespace AopProxy.AOP.Advice
{
    public class TransactionAdvice : IAroundAdvice
    {
        public virtual object Invoke(InterceptorContext context)
        {
            TransactionAttribute transAttr = (context.TargetMethodInfo.GetCustomAttributes(typeof(TransactionAttribute), true) as TransactionAttribute[])[0];

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = transAttr.IsolationLevel;
            options.Timeout = transAttr.TimeOut;
            using (TransactionScope tran = new TransactionScope(transAttr.ScopeOption, options, transAttr.EnterpriseServicesInteropOption))
            {
                object returnValue = context.Invoke();
                tran.Complete();
                return returnValue;
            }
        }
    }
}
