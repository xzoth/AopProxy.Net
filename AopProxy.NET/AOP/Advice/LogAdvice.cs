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
            Console.WriteLine(string.Format("Before Invoke in {0}::{1}", context.MethodInfo.Name));
        }

        public override object Invoke(InterceptorContext context)
        {
            return base.Invoke(context);
        }

        public override void AfterInvoke(InterceptorContext context)
        {
            //TODO: 记录日志
            base.AfterInvoke(context);
        }
    }
}
