using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP.Advice
{
    public abstract class AroundAdvice : IAroundAdvice 
    {
        public abstract object Invoke(InterceptorContext context);
    }
}
