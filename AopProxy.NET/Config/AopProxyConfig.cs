using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AopProxy.Config
{
    public class AopProxyConfig
    {
        public AopProxyConfig()
        {
            Advisors = new List<AdvisorConfig>();
        }

        public List<AdvisorConfig> Advisors { get; set; }
    }
}
