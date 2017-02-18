using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AopProxy.AOP
{
    public interface IAroundAdvice
    {
        void BeforeInvoke(MethodInfo methodInfo, object[] args, object target);

        void AfterInvoke(MethodInfo methodInfo, object[] args, object target);
    }
}
