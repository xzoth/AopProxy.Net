using AopProxy.AOP;
using AopProxy.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.Config
{
    public class PointCutConfig
    {
        public PointCutConfig()
        {
            Advice = new List<IAdvice>();
        }

        public string JoinPoint { get; set; }

        List<IAdvice> Advice { get; set; }
    }
}
