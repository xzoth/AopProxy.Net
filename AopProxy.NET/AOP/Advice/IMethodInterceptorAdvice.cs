using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP.Advice
{
    public interface IMethodInterceptorAdvice : IAdvice
    {
        object Invoke(InterceptorContext context);
    }
}
