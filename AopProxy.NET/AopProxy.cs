using AopProxy.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace AopProxy
{
    public class AopProxy<T> : RealProxy
    {
        private T targetInstance;

        public AopProxy(T targetInstance)
            : base(typeof(T))
        {
            this.targetInstance = targetInstance;
        }

        public T GetTargetInstance()
        {
            return targetInstance;
        }

        public T GetProxy()
        {
            return targetInstance;
        }

        /// <summary>
        /// 拦截所有方法的调用；
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage message)
        {
            IMethodCallMessage methodCallMessage = message as IMethodCallMessage;
            //TODO: 移除LAMBDA表达式以降低对framework版本的要求
            Type[] argsType = methodCallMessage.MethodBase.GetParameters().Select(t => t.ParameterType).ToArray();

            Type targetType = targetInstance.GetType();
            var methodInfo = targetType.GetMethod(methodCallMessage.MethodBase.Name, argsType);
            var attributes = methodInfo.GetCustomAttributes(typeof(JoinPointAttribute), true);
            if (attributes != null && attributes.Length > 0)
            {
                
            }
            else
            {

            }

            BeforeInvoke(methodCallMessage.MethodBase);

            try
            {
                object retValue = methodCallMessage.MethodBase.Invoke(targetInstance, methodCallMessage.Args);
                return new ReturnMessage(retValue, methodCallMessage.Args, methodCallMessage.ArgCount - methodCallMessage.InArgCount, methodCallMessage.LogicalCallContext, methodCallMessage);
            }
            catch (Exception ex)
            {
                return new ReturnMessage(ex, methodCallMessage);
            }
            finally
            {
                //调用后处理；
                AfterInvoke(methodCallMessage.MethodBase);
            }
        }

        private void BeforeInvoke(MethodBase method)
        {
            Console.WriteLine("Before Invoke {0}::{1}", targetInstance.GetType().FullName, method.ToString());
        }

        private void AfterInvoke(MethodBase method)
        {
            Console.WriteLine("After Invoke {0}::{1}", targetInstance.GetType().FullName, method.ToString());
        }
    }
}
