using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AopProxy.Config
{
    [Serializable]
    [XmlType("Advisor")]
    public class AdvisorConfig
    {
        [XmlAttribute("PointCutType")]
        public string PointCutType { get; set; }

        [XmlAttribute("AdviseType")]
        public string AdviseType { get; set; }
    }
}
