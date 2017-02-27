using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP.Attribute
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ThrowsAttribute : JoinPointAttribute
    {
        public ThrowsAttribute() : base()
        {

        }

        public ThrowsAttribute(string Code = "", string Message = "") : this()
        {
            this.Code = Code;
            this.Message = Message;
        }

        public virtual string Code { get; protected set; }

        public virtual string Message { get; protected set; }
    }
}
