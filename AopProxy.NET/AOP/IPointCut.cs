using AopProxy.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.AOP
{
    public interface IPointCut
    {
        IAdvice Advice { get; set; }

        JoinPointAttribute JointPoint { get; set; }
    }
}
