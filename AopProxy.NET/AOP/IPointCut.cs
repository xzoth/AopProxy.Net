using AopProxy.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP
{
    public interface IPointCut
    {
        JoinPointAttribute JoinPoint { get; set; }
        IAdvice[] Advice { get; set; }
    }
}
