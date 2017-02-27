using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace AopProxy.AOP.Config
{
    public class AopProxyConfigSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            using (StringReader reader = new StringReader(section.OuterXml))
            {
                AopProxyConfig config = AopProxyConfig.Load(reader);
                return config;
            }
        }
    }
}
