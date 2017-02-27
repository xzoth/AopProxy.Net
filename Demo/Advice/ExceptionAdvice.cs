using AopProxy.AOP.Advice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopProxy.AOP;

namespace Demo.Advice
{
    public class ExceptionAdvice : IThrowsAdvice
    {
        public virtual void OnException(InterceptorContext context, Exception e)
        {
            string strErrMsg = string.Format("Error in: {0}::{1} \r\n Args: {2}  \r\n Error Message: {3}", context.TargetMethodInfo.DeclaringType, context.TargetMethodInfo, e.InnerException.Message);
            Console.WriteLine(strErrMsg);
        }
    }
}
