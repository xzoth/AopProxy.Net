using AopProxy;
using AopProxy.AOP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //ILogic obj = AopProxyFactory.GetProxy<ILogic>();
            ILogic obj = AopProxyFactory.GetProxy<ILogic, LogicObject>();
            obj.Add(3, 3);
            obj.Add(-2, 12);
            int result = obj.Result;
            obj.ShowResult();
            Console.WriteLine("*********************************************");

            Console.WriteLine("start performance test, please input invoke times:");
            string strTimes = Console.ReadLine();
            int times = Convert.ToInt32(strTimes);

            Stopwatch watcher = new Stopwatch();
            watcher.Start();

            for(float i=0; i<times; i++)
            {
                obj.Add(i, i);
            }

            watcher.Stop();


            TimeSpan time = watcher.Elapsed;

            double total = watcher.Elapsed.TotalMilliseconds;
            double preTimes = total / times;

            Console.WriteLine("total: {0} \tPreTimes: {1}", total, preTimes);
            Console.Read();
        }
    }
}
