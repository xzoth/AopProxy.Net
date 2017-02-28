using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy
{
    public class ExceptionBase : global::System.Exception
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

        public ExceptionBase() : base()
        {
            this.Code = string.Empty;
        }

        public ExceptionBase(string message) : base(message) { }

        public ExceptionBase(string message, string code)
            : this(message)
        {
            this.Code = code;
        }

        public ExceptionBase(string message, global::System.Exception innerException) : base(message, innerException) { }

        public ExceptionBase(string message, string code, global::System.Exception innerException)
            : this(message, innerException)
        {
            this.Code = code;
        }
    }
}
