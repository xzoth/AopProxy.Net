using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    public class LogicObject : ILogic
    {
        public LogicObject() { }

        public int Add(int a, int b)
        {
            return result = a + b;
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
