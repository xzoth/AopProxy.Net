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
        private T transparentProxy;

        public AopProxy(T transparentProxy)
            : base(typeof(T))
        {
            this.transparentProxy = transparentProxy;
        }

        public T GetProxy()
        {
            return transparentProxy;
        }

        /// <summary>
        /// 拦截所有方法的调用；
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage callMsg = msg as IMethodCallMessage;
            BeforeInvoke(callMsg.MethodBase);

            try
            {
                object retValue = callMsg.MethodBase.Invoke(transparentProxy, callMsg.Args);
                return new ReturnMessage(retValue, callMsg.Args, callMsg.ArgCount - callMsg.InArgCount, callMsg.LogicalCallContext, callMsg);
            }
            catch (Exception ex)
            {
                return new ReturnMessage(ex, callMsg);
            }
            finally
            {
                //调用后处理；
                AfterInvoke(callMsg.MethodBase);
            }
        }

        private void BeforeInvoke(MethodBase method)
        {
            Console.WriteLine("Before Invoke {0}::{1}", transparentProxy.GetType().FullName, method.ToString());
        }

        private void AfterInvoke(MethodBase method)
        {
            Console.WriteLine("After Invoke {0}::{1}", transparentProxy.GetType().FullName, method.ToString());
        }
    }
}
