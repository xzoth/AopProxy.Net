﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.Config
{
    public class AopProxyConfig
    {
        public AopProxyConfig()
        {
            PointCut = new List<PointCutConfig>();
        }

        List<PointCutConfig> PointCut { get; set; }
    }
}
