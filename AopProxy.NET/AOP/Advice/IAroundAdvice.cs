using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AopProxy.AOP.Advice
{
    public interface IAroundAdvice: IMethodInterceptorAdvice
    {
        void BeforeInvoke(InterceptorContext context);

        void AfterInvoke(InterceptorContext context);
    }
}
