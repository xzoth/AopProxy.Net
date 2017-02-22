using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP.Advice
{
    public class LogAdvice : AroundAdvice
    {
        public override void BeforeInvoke(InterceptorContext context)
        {
            Console.WriteLine(string.Format("Before Invoke in {0}", context.MethodInfo.Name));
        }

        public override object Invoke(InterceptorContext context)
        {
            object returnValue = context.Invoke();
            Console.WriteLine(string.Format("Go in Method, return {0}", returnValue));
            return returnValue;
        }

        public override void AfterInvoke(InterceptorContext context)
        {
            Console.WriteLine(string.Format("After Invoke in {0}", context.MethodInfo.Name));
        }
    }
}
