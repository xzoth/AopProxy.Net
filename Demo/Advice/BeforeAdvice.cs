using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopProxy.AOP;
using AopProxy.AOP.Advice;

namespace Demo.Advice
{
    public class BeforeAdvice : IBeforeAdvice
    {
        public void BeforeInvoke(InterceptorContext context)
        {
            Console.WriteLine("before enter method oh yeah");
        }
    }
}
