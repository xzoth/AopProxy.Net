using AopProxy.AOP;
using AopProxy.AOP.Advice;
using AopProxy.AOP.Attribute;
using AopProxy.AOP.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AopProxy.AOP
{
    public class AopProxyFactory
    {
        private static AopProxyFactory instance = new AopProxyFactory();

        private AopProxyFactory()
        {
            Config = AopProxyConfig.Load();
            if (Config == null)
            {
                Config = new AopProxyConfig();
            }

            TypeMap = new Dictionary<Type, IAdvice>();
            foreach (var advConfig in Config.Advisors)
            {
                Type pointCutType = AopProxyFactory.LoadType(advConfig.PointCutType);
                Type adviceType = AopProxyFactory.LoadType(advConfig.AdviseType);
                IAdvice advice = Activator.CreateInstance(adviceType) as IAdvice;

                TypeMap[pointCutType] = advice;
            }
        }

        public static AopProxyFactory Instance
        {
            get
            {
                return instance;
            }
            protected set
            {
                instance = value;
            }
        }

        public static T GetProxy<T, K>() where K : class
        {
            Type interfaceType = typeof(T);
            Type targetType = typeof(K);
            if (!interfaceType.IsInterface)
            {
                throw new Exception("Interface Only");
            }

            if (interfaceType.IsAssignableFrom(targetType))
            {
                var targetInstance = (T)Activator.CreateInstance(targetType);
                T proxy = GetTransparentProxyByInstance<T>(targetInstance);
                return proxy;
            }
            else
            {
                return default(T);
            }
        }

        public static T GetProxy<T>()
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
                T proxy = GetTransparentProxyByInstance<T>(targetInstance);
                return proxy;
            }
            else
            {
                return default(T);
            }
        }

        public static T GetTransparentProxyByInstance<T>(T targetInstance)
        {
            AopProxy<T> proxy = new AopProxy<T>(targetInstance);
            return (T)proxy.GetTransparentProxy();
        }

        public AopProxyConfig Config { get; set; }
        internal Dictionary<Type, IAdvice> TypeMap { get; set; }

        public static Type LoadType(string typeString)
        {
            return Type.GetType(typeString, true, true);
        }
    }
}
