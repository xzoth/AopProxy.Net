using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP.Advice
{
    public abstract class AroundAdvice : MethodInterceptorAdvice, IAroundAdvice 
    {
        public virtual void AfterInvoke(InterceptorContext context)
        {
        }

        public virtual void BeforeInvoke(InterceptorContext context)
        {
        }
    }
}
