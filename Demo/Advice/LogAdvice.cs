using AopProxy.AOP;
using AopProxy.AOP.Advice;
using AopProxy.AOP.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Advice
{
    public class LogAdvice : IAroundAdvice, IBeforeAdvice, IAfterAdvice
    {
        public void AfterInvoke(InterceptorContext context)
        {
            Console.WriteLine(string.Format("After Invoke in {0}::{1}", context.TargetMethodInfo.DeclaringType.FullName, context.TargetMethodInfo));
        }

        public void BeforeInvoke(InterceptorContext context)
        {
            string strArgs = string.Empty;
            foreach (var arg in context.Args)
            {
                strArgs += arg + " ";
            }
            Console.WriteLine(string.Format("Before Invoke in {0}::{1} Parameters: {2}", context.TargetMethodInfo.DeclaringType.FullName, context.TargetMethodInfo, strArgs));

            LogAttribute logAttr = (context.TargetMethodInfo.GetCustomAttributes(typeof(LogAttribute), false) as LogAttribute[])[0];
            Console.WriteLine("Log Level: {0}", logAttr.Level);
        }

        public virtual object Invoke(InterceptorContext context)
        {
            object returnValue = context.Invoke();
            Console.WriteLine(string.Format("processed, return value: {0}", returnValue));
            return returnValue;
        }
    }
}
