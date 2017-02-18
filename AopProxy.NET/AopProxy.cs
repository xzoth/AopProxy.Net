using AopProxy.AOP;
using AopProxy.Attribute;
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
    public delegate void BeforeInvokeEventHandler(MethodInfo methodInfo, object[] args, object targetInstance);
    public delegate void AfterInvokeEventHandler(MethodInfo methodInfo, object returnValue, object targetInstance);
    public delegate void ExceptionEventHandler(MethodInfo methodInfo, Exception e, object targetInstance);

    public class AopProxy<T> : RealProxy
    {
        public event BeforeInvokeEventHandler BeforeInvoke;
        public event AfterInvokeEventHandler AfterInvoke;
        public event ExceptionEventHandler Exception;

        public AopProxy(T targetInstance)
            : base(typeof(T))
        {
            this.targetInstance = targetInstance;

            BeforeInvoke += OnBeforeInvoke;
            AfterInvoke += OnAfterInvoke;
        }

        public void RaiseAfterInvokeEvent(MethodInfo methodInfo, object returnValue)
        {
            AfterInvoke(methodInfo, returnValue, targetInstance);
        }

        protected virtual void OnAfterInvoke(MethodInfo methodInfo, object returnValue, object targetInstance)
        {

        }

        public void RaiseBeforeInvokeEvent(MethodInfo methodInfo, object[] args)
        {
            BeforeInvoke(methodInfo, args, targetInstance);
        }

        protected virtual void OnBeforeInvoke(MethodInfo methodInfo, object[] args, object targetInstance)
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

        public override IMessage Invoke(IMessage message)
        {
            IMethodCallMessage methodCallMessage = message as IMethodCallMessage;
            MethodInfo mInfo = methodCallMessage.MethodBase as MethodInfo;
            object returnValue = null;
            try
            {
                
                RaiseBeforeInvokeEvent(mInfo, methodCallMessage.Args);
                returnValue = methodCallMessage.MethodBase.Invoke(targetInstance, methodCallMessage.Args);

                return new ReturnMessage(returnValue, methodCallMessage.Args, methodCallMessage.ArgCount - methodCallMessage.InArgCount, methodCallMessage.LogicalCallContext, methodCallMessage);
            }
            catch (Exception e)
            {
                //TODO 异常处理
                return new ReturnMessage(e.InnerException, methodCallMessage);
            }
            finally
            {
                RaiseAfterInvokeEvent(mInfo, returnValue);
            }
        }
    }
}
