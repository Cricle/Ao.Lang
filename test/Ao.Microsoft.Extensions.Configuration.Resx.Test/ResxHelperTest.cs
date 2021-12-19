using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Microsoft.Extensions.Configuration.Resx.Test
{
    [TestClass]
    public class ResxHelperTest
    {
        private static readonly string ResxPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Resource1.resx");
        private void Check(IDictionary<string, string> map)
        {
            var title = map["Title"];
            var name = map["Name"];
            Assert.AreEqual("title", title);
            Assert.AreEqual("name", name);
            Assert.AreEqual(2, map.Count);
        }
        [TestMethod]
        public void GivenStream_MustParseMap()
        {
            using (var fs = File.OpenRead(ResxPath))
            {
                var map = ResxHelper.GetData(fs);
                Check(map);
            }
        }
        [TestMethod]
        public void GivenString_MustParseMap()
        {
            var content = File.ReadAllText(ResxPath);
            var map = ResxHelper.GetData(content);
            Check(map);
        }
        [TestMethod]
        public void GivenDoc_MustParseMap()
        {
            var doc = new XmlDocument();
            doc.Load(ResxPath);
            var map = ResxHelper.GetData(doc);
            Check(map);
        }
    }
}
