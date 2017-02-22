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

            BeforeInvoke += OnBeforeInvoke;
            AfterInvoke += OnAfterInvoke;
            ThrowException += OnThrowException;
        }

        public void RaiseBeforeInvokeEvent(InterceptorContext context)
        {
            BeforeInvoke(context);
        }

        public void RaiseAfterInvokeEvent(InterceptorContext context)
        {
            AfterInvoke(context);
        }

        public void RaiseThrowException(InterceptorContext context, Exception e)
        {
            ThrowException(context, e);
        }

        protected virtual void OnBeforeInvoke(InterceptorContext context)
        {

        }

        protected virtual void OnAfterInvoke(InterceptorContext context)
        {

        }

        protected virtual void OnThrowException(InterceptorContext context, Exception e)
        {
        }

        private T targetInstance;
        public T TargetInstance
        {
            get
            {
                return targetInstance;
            }
        }

        public AopProxyConfig Config { get; set; }

        public override IMessage Invoke(IMessage message)
        {
            IMethodCallMessage methodCallMessage = message as IMethodCallMessage;
            MethodInfo mInfo = methodCallMessage.MethodBase as MethodInfo;

            //构造类型配置集合
            Dictionary<Type, Type> Cfg = new Dictionary<Type, Type>();

            Config = new AopProxy.Config.AopProxyConfig();
            Config.Advisors.Add(new AdvisorConfig()
            {
                AdviseType = "AopProxy.AOP.Advice.LogAdvice, AopProxy",
                PointCutType = "AopProxy.AOP.Attribute.LogAttribute, AopProxy"
            });
            foreach (var advConfig in Config.Advisors)
            {
                Type pointCutType = AopProxyFactory.LoadType(advConfig.PointCutType);
                Type adviseType = AopProxyFactory.LoadType(advConfig.AdviseType);

                Cfg[pointCutType] = adviseType;
            }

            var argsType = mInfo.GetParameters().Select(t => t.ParameterType).ToArray();
            Type targetType = targetInstance.GetType();
            var realMethodInfo = targetType.GetMethod(mInfo.Name, argsType);
            var attributes = realMethodInfo.GetCustomAttributes(typeof(JoinPointAttribute), true);

            List<AroundAdvice> AroundAdviceList = new List<AroundAdvice>();
            foreach (JoinPointAttribute attribute in attributes)
            {
                Type joinPointType = attribute.GetType();
                Type[] matchTypes = Cfg.Where(item => item.Key.IsAssignableFrom(joinPointType)).Select(item => item.Value).ToArray();

                foreach (Type matchAdviceType in matchTypes)
                {
                    AroundAdvice advice = Activator.CreateInstance(matchAdviceType) as AroundAdvice;
                    BeforeInvoke += advice.BeforeInvoke;
                    AfterInvoke += advice.AfterInvoke;

                    AroundAdviceList.Add(advice);
                }
            }


            InterceptorContext context = new AOP.InterceptorContext()
            {
                TargetInstance = targetInstance,
                Args = methodCallMessage.Args,
                MethodInfo = mInfo
            };

            try
            {
                RaiseBeforeInvokeEvent(context);
                foreach (var advice in AroundAdviceList)
                {
                    advice.Invoke(context);
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
                RaiseAfterInvokeEvent(context);
            }
        }
    }
}
