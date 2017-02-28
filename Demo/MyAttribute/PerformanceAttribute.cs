using AopProxy.AOP.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.MyAttribute
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class PerformanceAttribute : AroundAttribute
    {
    }
}
