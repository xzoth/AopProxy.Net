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
        private static AopProxyFactory instance = new AopProxyFactory();
        
        private AopProxyFactory()
        {
            Config = new AopProxyConfig();
            Config.Advisors.Add(new AdvisorConfig()
            {
                AdviseType = "AopProxy.AOP.Advice.LogAdvice, AopProxy",
                PointCutType = "AopProxy.AOP.Attribute.LogAttribute, AopProxy"
            });

            TypeMap = new Dictionary<Type, AroundAdvice>();
            foreach (var advConfig in AopProxyFactory.Config.Advisors)
            {
                Type pointCutType = AopProxyFactory.LoadType(advConfig.PointCutType);
                Type adviceType = AopProxyFactory.LoadType(advConfig.AdviseType);
                AroundAdvice advice = Activator.CreateInstance(adviceType) as AroundAdvice;

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
                return (T)proxy.GetTransparentProxy();
            }
            else
            {
                return default(T);
            }
        }

        public static AopProxyConfig Config { get; set; }
        internal Dictionary<Type, AroundAdvice> TypeMap { get; set; }

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
