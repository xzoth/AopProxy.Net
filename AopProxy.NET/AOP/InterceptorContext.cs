using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AopProxy.AOP
{
    public class InterceptorContext
    {
        public object TargetInstance { get; internal set; }

        public MethodInfo TargetMethodInfo { get; internal set; }

        public MethodInfo MethodInfo { get; internal set; }

        public object[] Args { get; internal set; }

        private static object locker = new object();

        public object Invoke()
        {
            NextHandler = GetNextHandler();
            if (NextHandler != null)
            {
                return NextHandler(this);
            }
            else
            {
                return MethodInfo.Invoke(TargetInstance, Args);
            }
        }
        
        internal Queue<AroundAdviceHandler> MethodChain = new Queue<AroundAdviceHandler>();

        internal AroundAdviceHandler NextHandler;

        internal AroundAdviceHandler GetNextHandler()
        {
            if (MethodChain.Count > 0)
            {
                return MethodChain.Dequeue();
            }
            return null;
        }
    }

    public delegate object AroundAdviceHandler(InterceptorContext context);
}
