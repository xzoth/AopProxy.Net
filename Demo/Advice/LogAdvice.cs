using AopProxy.AOP;
using AopProxy.AOP.Advice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Advice
{
    public class LogAdvice : IAroundAdvice
    {
        public virtual object Invoke(InterceptorContext context)
        {
            Console.WriteLine(string.Format("Before Invoke in {0}::{1}", context.TargetMethodInfo.DeclaringType.FullName, context.TargetMethodInfo));

            string strArgs = string.Empty;
            foreach (var arg in context.Args)
            {
                strArgs += arg + " ";
            }
            Console.WriteLine(string.Format("Parameters: {0}", strArgs));



            object returnValue = context.Invoke();
            Console.WriteLine(string.Format("processed, return value: {0}", returnValue));



            Console.WriteLine(string.Format("After Invoke in {0}::{1}", context.TargetMethodInfo.DeclaringType.FullName, context.TargetMethodInfo));

            return returnValue;
        }
    }
}
