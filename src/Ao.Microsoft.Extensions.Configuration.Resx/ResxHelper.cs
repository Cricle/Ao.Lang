using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Microsoft.Extensions.Configuration.Resx
{
    public class ResxStreamConfigurationProvider : StreamConfigurationProvider
    {
        public ResxStreamConfigurationProvider(StreamConfigurationSource source)
            : base(source)
        {
        }

        public override void Load(Stream stream)
        {
            Data = ResxHelper.GetData(stream);
        }
    }

    public class ResxStreamConfigurataionSource : StreamConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ResxStreamConfigurationProvider(this);
        }
    }

    public class ResxConfigurationProvider : FileConfigurationProvider
    {
        public ResxConfigurationProvider(FileConfigurationSource source)
            : base(source)
        {
        }

        public override void Load(Stream stream)
        {
            Data = ResxHelper.GetData(stream);
        }
    }

    public class ResxConfigurationSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ResxConfigurationProvider(this);
        }
    }

    internal static class ResxHelper
    {
        public static IDictionary<string, string> GetData(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var sr = new StreamReader(stream))
            {
                var content = sr.ReadToEnd();
                return GetData(content);
            }
        }

        public static IDictionary<string, string> GetData(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException($"“{nameof(content)}”不能为 null 或空。", nameof(content));
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(content);
            return GetData(xmlDoc);
        }

        public static IDictionary<string, string> GetData(XmlDocument doc)
        {
            var root = doc.ChildNodes.OfType<XmlNode>().FirstOrDefault(x => x.Name == "root");
            var map = new Dictionary<string, string>();
            if (root != null)
            {
                var datas = root.ChildNodes.OfType<XmlNode>().Where(x => x.Name == "data");
                foreach (var item in datas)
                {
                    var name = item.Attributes["name"].Value;
                    var value = item.InnerText.Trim();
                    map.Add(name, value);
                }
            }
            return map;
        }
    }
}