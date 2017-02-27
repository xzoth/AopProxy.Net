using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP.Attribute
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class LogAttribute : AroundAttribute
    {
        public LogAttribute(LogLevel level = LogLevel.Error) : base()
        {
            this.Level = level;
        }

        public virtual LogLevel Level { get; protected set; }
    }
}
