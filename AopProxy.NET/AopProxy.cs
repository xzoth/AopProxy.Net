using AopProxy.AOP;
using AopProxy.AOP.Advice;
using AopProxy.AOP.Attribute;
using AopProxy.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace AopProxy
{
    public delegate void BeforeInvokeEventHandler(InterceptorContext context);
    public delegate void AfterInvokeEventHandler(InterceptorContext context);
    public delegate void ExceptionEventHandler(InterceptorContext context, Exception e);

    public class AopProxy<T> : RealProxy
    {
        public event BeforeInvokeEventHandler BeforeInvoke;
        public event AfterInvokeEventHandler AfterInvoke;
        public event ExceptionEventHandler ThrowException;

        public AopProxy(T targetInstance)
            : base(typeof(T))
        {
            this.targetInstance = targetInstance;
        }

        public void RaiseBeforeInvokeEvent(InterceptorContext context)
        {
            if (BeforeInvoke != null)
            {
                BeforeInvoke(context);
            }
        }

        public void RaiseAfterInvokeEvent(InterceptorContext context)
        {
            if (AfterInvoke != null)
            {
                AfterInvoke(context);
            }
        }

        public void RaiseThrowException(InterceptorContext context, Exception e)
        {
            if (ThrowException != null)
            {
                ThrowException(context, e);
            }
        }

        private T targetInstance;
        public T TargetInstance
        {
            get
            {
                return targetInstance;
            }
        }

        public override IMessage Invoke(IMessage message)
        {
            IMethodCallMessage methodCallMessage = message as IMethodCallMessage;
            MethodInfo messageMethodInfo = methodCallMessage.MethodBase as MethodInfo;

            var argsType = messageMethodInfo.GetParameters().Select(t => t.ParameterType).ToArray();
            Type targetType = targetInstance.GetType();
            var targetMethodInfo = targetType.GetMethod(messageMethodInfo.Name, argsType);
            var methodAttributes = targetMethodInfo.GetCustomAttributes(typeof(JoinPointAttribute), true);

            List<AroundAdvice> AroundAdviceList = new List<AroundAdvice>();
            foreach (JoinPointAttribute attr in methodAttributes)
            {
                Type joinPointType = attr.GetType();
                AroundAdviceList = AopProxyFactory.TypeMap.Where(item => item.Key.IsAssignableFrom(joinPointType)).Select(item => item.Value).ToList();

                foreach (var advice in AroundAdviceList)
                {
                    BeforeInvoke += advice.BeforeInvoke;
                    AfterInvoke += advice.AfterInvoke;
                }
            }

            InterceptorContext context = new InterceptorContext()
            {
                TargetInstance = targetInstance,
                Args = methodCallMessage.Args,
                MethodInfo = messageMethodInfo,
                TargetMethodInfo = targetMethodInfo
            };

            try
            {
                if (AroundAdviceList.Count > 0)
                {
                    RaiseBeforeInvokeEvent(context);
                    foreach (var advice in AroundAdviceList)
                    {
                        BeforeInvoke -= advice.BeforeInvoke;
                        advice.Invoke(context);
                    }
                }
                else
                {
                    context.Invoke();
                }

                return new ReturnMessage(context.ReturnValue, methodCallMessage.Args, methodCallMessage.ArgCount - methodCallMessage.InArgCount, methodCallMessage.LogicalCallContext, methodCallMessage);
            }
            catch (Exception e)
            {
                RaiseThrowException(context, e);
                return new ReturnMessage(e.InnerException, methodCallMessage);
            }
            finally
            {
                if (AroundAdviceList.Count > 0)
                {
                    RaiseAfterInvokeEvent(context);
                    foreach (var advice in AroundAdviceList)
                    {
                        AfterInvoke -= advice.AfterInvoke;
                    }
                }
            }
        }
    }
}
