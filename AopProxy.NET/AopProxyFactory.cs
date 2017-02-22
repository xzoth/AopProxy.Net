using AopProxy.AOP;
using AopProxy.AOP.Advice;
using AopProxy.AOP.Attribute;
using AopProxy.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AopProxy
{
    public class AopProxyFactory
    {
        public static T GetProxy<T>()
        {
            return GetProxy<T>(false);
        }

        public static T GetProxy<T>(bool isSingle)
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
                var targetInstance = (T)Activator.CreateInstance(types[0]);
                AopProxy<T> proxy = new AopProxy<T>(targetInstance);

                //TODO 根据Advisor织入定义，绑定事件处理Advice方法。
                proxy.BeforeInvoke += Proxy_BeforeInvoke;
                proxy.AfterInvoke += Proxy_AfterInvoke;
                proxy.ThrowException += Proxy_ThrowException;

                return (T)proxy.GetTransparentProxy();
            }
            else
            {
                return default(T);
            }
        }

        private static void Proxy_ThrowException(InterceptorContext context, Exception e)
        {
            
        }

        private static void Proxy_BeforeInvoke(InterceptorContext context)
        {
            
        }

        private static void Proxy_AfterInvoke(InterceptorContext context)
        {
            //TODO: 缓存目标实例和相关元数据以加强性能

            //IMethodCallMessage methodCallMessage = message as IMethodCallMessage;
            ////TODO: 移除LAMBDA表达式以降低对framework版本的要求
            //Type[] argsType = methodCallMessage.MethodBase.GetParameters().Select(t => t.ParameterType).ToArray();

            //Type targetType = targetInstance.GetType();
            //var methodInfo = targetType.GetMethod(methodCallMessage.MethodBase.Name, argsType);
            //var attributes = methodInfo.GetCustomAttributes(typeof(JoinPointAttribute), true);
        }

        public AopProxyConfig Config { get; set; }

        //public static void AddAdvisor(string strAdviceType, string strPointCutType)
        //{
        //    Type adviceType = LoadType(strAdviceType);
        //    Type pointcutType = LoadType(strPointCutType);
        //}

        public static Type LoadType(string typeString)
        {
            return Type.GetType(typeString, true, true);
        }
    }
}
