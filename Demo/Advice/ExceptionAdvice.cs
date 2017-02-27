using AopProxy.AOP.Advice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopProxy.AOP;
using AopProxy.AOP.Attribute;

namespace Demo.Advice
{
    public class ExceptionAdvice : IThrowsAdvice
    {
        public virtual void OnException(InterceptorContext context, Exception e)
        {
            ThrowsAttribute throwAttr = (context.TargetMethodInfo.GetCustomAttributes(typeof(ThrowsAttribute), false) as ThrowsAttribute[])[0];

            string strArgs = string.Empty;
            foreach (var arg in context.Args)
            {
                strArgs += arg + " ";
            }
            string strErrMsg = string.Format("Error in: {0}::{1} \r\nArgs: {2}  \r\nError Message: {3}\r\nCode: {4}", 
                context.TargetMethodInfo.DeclaringType, 
                context.TargetMethodInfo, 
                strArgs, 
                e.InnerException.Message,
                throwAttr.Code);

            Console.WriteLine(strErrMsg);
        }
    }
}
