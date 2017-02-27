using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy
{
    public class AopExceptionBase : global::System.Exception
    {
        public virtual string Code
        {
            get;
            protected set;
        }

        public override string Message
        {
            get
            {
                return base.Message;
            }
        }

        public AopExceptionBase() : base()
        {
            this.Code = string.Empty;
        }

        public AopExceptionBase(string message) : base(message) { }

        public AopExceptionBase(string message, string code)
            : this(message)
        {
            this.Code = code;
        }

        public AopExceptionBase(string message, global::System.Exception innerException) : base(message, innerException) { }

        public AopExceptionBase(string message, string code, global::System.Exception innerException)
            : this(message, innerException)
        {
            this.Code = code;
        }
    }
}
