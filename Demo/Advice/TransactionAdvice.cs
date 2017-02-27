using AopProxy.AOP.Advice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopProxy.AOP;

namespace Demo.Advice
{
    public class TransactionAdvice : IAroundAdvice
    {
        public virtual object Invoke(InterceptorContext context)
        {
            //TODO:开启数据库事务

            object returnValue = context.Invoke();
            return returnValue;
        }
    }
}
