using AopProxy.AOP.Advice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopProxy.AOP;

namespace Demo.Advice
{
    public class AuthAdvice : IAroundAdvice
    {
        public virtual object Invoke(InterceptorContext context)
        {
            return context.Invoke();
        }
    }
}
