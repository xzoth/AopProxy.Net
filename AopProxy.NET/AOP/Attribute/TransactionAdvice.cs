using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP.Advice
{
    public class TransactionAdvice : IAroundAdvice
    {
        public object Invoke(InterceptorContext context)
        {
            //TODO:开启数据库事务

            object returnValue = context.Invoke();
            return returnValue;
        }
    }
}
