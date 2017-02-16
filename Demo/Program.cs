using AopProxy;
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
}
