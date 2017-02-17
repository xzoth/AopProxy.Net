using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AopProxy.AOP
{
    public interface IThrowsAdvice : IAdvice
    {
        void OnThrow(MethodInfo methodInfo, object[] args, object target, Exception e);
    }
}
