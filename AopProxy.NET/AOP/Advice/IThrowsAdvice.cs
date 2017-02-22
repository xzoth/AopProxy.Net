
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AopProxy.AOP.Advice
{
    public interface IThrowsAdvice : IAdvice
    {
        void OnException(InterceptorContext context, Exception e);
    }
}
