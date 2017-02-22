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
            Console.WriteLine(string.Format("Before Invoke in {0}::{1}", context.MethodInfo.DeclaringType.FullName, context.MethodInfo));

            string strArgs = string.Empty;
            foreach(var arg in context.Args)
            {
                strArgs += arg + " ";
            }
            Console.WriteLine(string.Format("Parameters {0}", strArgs));
        }

        public override object Invoke(InterceptorContext context)
        {
            object returnValue = context.Invoke();
            Console.WriteLine(string.Format("Go in Method, return {0}", context.ReturnValue));
            return returnValue;
        }

        public override void AfterInvoke(InterceptorContext context)
        {
            Console.WriteLine(string.Format("After Invoke in {0}::{1}", context.MethodInfo.DeclaringType.FullName, context.MethodInfo));
        }
    }
}
