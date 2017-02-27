using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AopProxy.AOP.Config
{
    [Serializable]
    public abstract class ConfigBase<T> where T : ConfigBase<T>
    {
        [XmlIgnore]
        public string FilePath { get; protected set; }

        public ConfigBase() { }
        
        public static T Load(string strPath)
        {
            T config = null;

            using (FileStream file = new FileStream(strPath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                config = serializer.Deserialize(file) as T;
                config.FilePath = strPath;
            }

            return config;
        }
        
        public static T Load(TextReader reader)
        {
            T config = null;

            if (reader != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                config = serializer.Deserialize(reader) as T;
            }

            return config;
        }

        public void Save()
        {
            Save(FilePath);
        }
        
        public void Save(string strPath)
        {
            using (FileStream fileStream = new FileStream(strPath, FileMode.Create))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = false;//是否生成XML声明头
                settings.Indent = true;
                settings.NewLineChars = "\r\n";
                settings.Encoding = Encoding.UTF8;
                settings.IndentChars = "    ";

                using (XmlWriter writer = XmlWriter.Create(fileStream, settings))
                {
                    XmlSerializer serializer = new XmlSerializer(this.GetType());

                    // 强制指定命名空间，覆盖默认的命名空间。
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty);

                    serializer.Serialize(writer, this, namespaces);
                }
                fileStream.Flush(true);
            }
        }
        
        public string ToXML()
        {
            string strXML = string.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = false;
                settings.Indent = true;
                settings.NewLineChars = "\r\n";
                settings.Encoding = Encoding.UTF8;
                settings.IndentChars = "    ";

                using (XmlWriter writer = XmlWriter.Create(sw, settings))
                {
                    XmlSerializer serializer = new XmlSerializer(this.GetType());
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty);
                    serializer.Serialize(writer, this, namespaces);
                    writer.Flush();
                    writer.Close();
                }
                using (StreamReader sr = new StreamReader(ms))
                {
                    ms.Position = 0;
                    strXML = sr.ReadToEnd();
                    sr.Close();
                }
            }

            return strXML;
        }
    }
}
