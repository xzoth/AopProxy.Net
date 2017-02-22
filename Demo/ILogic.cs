using AopProxy.AOP.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    public interface ILogic
    {
        int Add(int a, int b);

        void ShowResult();

        int Result
        {
            get;
        }
    }
}
