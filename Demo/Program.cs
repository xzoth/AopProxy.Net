using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogic obj = AopProxyFactory.GetProxyByType<ILogic>();
            int result = obj.Add(3, 3);

            Console.WriteLine(string.Format("result is: {0}", result));
            Console.ReadLine();
        }
    }

    public interface ILogic
    {
        int Add(int a, int b);
    }

    public class LogicObject : ILogic
    {
        public LogicObject() { }

        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    public class AopProxyFactory
    {
        public static T GetProxyByType<T>()
        {
            Type interfaceType = typeof(T);
            if (!interfaceType.IsInterface)
            {
                throw new Exception("Interface Only");
            }

            Assembly[] asss = AppDomain.CurrentDomain.GetAssemblies();
            var types = asss
                        .SelectMany(a => a.GetTypes().Where(t => typeof(T).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract))
                        .ToArray();

            if (types != null && types.Length > 0)
            {
                var instance = (T)Activator.CreateInstance(types[0]);
                AopProxy<T> proxy = new AopProxy<T>(instance);
                return (T)proxy.GetTransparentProxy();
            }
            else
            {
                return default(T);
            }
        }
    }

    class AopProxy<T> : RealProxy
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
