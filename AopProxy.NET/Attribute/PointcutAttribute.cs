using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.Attribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class PointCutAttribute : System.Attribute
    {
    }
}
