using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP.Advice
{
    public class ExceptionAdvice : IThrowsAdvice
    {
        public virtual void OnException(InterceptorContext context, Exception e)
        {
        }
    }
}
