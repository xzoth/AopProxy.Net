using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP.Advice
{
    public abstract class MethodInterceptorAdvice : IMethodInterceptorAdvice
    {
        public virtual object Invoke(InterceptorContext context)
        {
            object returnValue = context.Invoke();
            return returnValue;
        }
    }
}
