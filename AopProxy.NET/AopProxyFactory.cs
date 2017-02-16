using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AopProxy
{
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
}
