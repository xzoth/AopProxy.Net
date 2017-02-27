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
            if(Config == null)
            {
                Config = new AopProxyConfig();
            }

            TypeMap = new Dictionary<Type, IAroundAdvice>();
            foreach (var advConfig in Config.Advisors)
            {
                Type pointCutType = AopProxyFactory.LoadType(advConfig.PointCutType);
                Type adviceType = AopProxyFactory.LoadType(advConfig.AdviseType);
                IAroundAdvice advice = Activator.CreateInstance(adviceType) as IAroundAdvice;

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
                AopProxy<T> proxy = new AopProxy<T>(targetInstance);
                return (T)proxy.GetTransparentProxy();
            }
            else
            {
                return default(T);
            }
        }

        public AopProxyConfig Config { get; set; }
        internal Dictionary<Type, IAroundAdvice> TypeMap { get; set; }

        public static Type LoadType(string typeString)
        {
            return Type.GetType(typeString, true, true);
        }
    }
}
