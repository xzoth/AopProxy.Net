using AopProxy.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    public class LogicObject : ILogic
    {
        public LogicObject() { }

        [JoinPoint]
        public int Add(int a, int b)
        {
            return result = a + b;
        }

        public float Add(float a, float b)
        {
            return a + b;
        }

        int result;
        public int Result
        {
            get
            {
                return result;
            }
        }
    }
}
