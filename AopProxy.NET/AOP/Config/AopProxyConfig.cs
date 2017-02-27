using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AopProxy.AOP.Config
{
    [Serializable]
    [XmlType("AopProxy")]
    [XmlRoot("AopProxy")]
    public class AopProxyConfig : ConfigBase<AopProxyConfig>
    {
        public static AopProxyConfig Load()
        {
            return ConfigurationManager.GetSection("AopProxy") as AopProxyConfig;
        }

        public AopProxyConfig()
        {
            Advisors = new List<AdvisorConfig>();
        }

        [XmlArray("Advisors")]
        public List<AdvisorConfig> Advisors { get; set; }
    }
}
