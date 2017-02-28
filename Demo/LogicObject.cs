using AopProxy;
using AopProxy.AOP.Attribute;
using Demo.MyAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    public class LogicObject : ILogic
    {
        public LogicObject() { }

        [Throws("BussinessExceptionCode")]
        [Log(LogLevel.Warn)]
        [Transaction(System.Transactions.TransactionScopeOption.Required, System.Transactions.IsolationLevel.Chaos, System.Transactions.EnterpriseServicesInteropOption.None, 10)]
        public int Add(int a, int b)
        {
            //throw new ArgumentException("参数TMD不正确");
            result = a + b;
            return result;
        }

        [Before]
        [Transaction]
        public void ShowResult()
        {
            Console.WriteLine(Result);
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
