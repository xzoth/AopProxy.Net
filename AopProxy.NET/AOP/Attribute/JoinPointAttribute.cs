using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP.Attribute
{
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class JoinPointAttribute : System.Attribute
    {
    }
}
