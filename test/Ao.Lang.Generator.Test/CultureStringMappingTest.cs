using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;

namespace Ao.Lang.Generator.Test
{
    [TestClass]
    public class CultureStringMappingTest
    {
        [TestMethod]
        public void InitWithConstruct()
        {
            var c = new CultureStringMapping(10);
            c = new CultureStringMapping();
            c = new CultureStringMapping(10);
            c = new CultureStringMapping(new Dictionary<string, string>(0));
        }
        [TestMethod]
        [DataRow("sdak")]
        [DataRow("hello")]
        public void AddCultureNotExists_MustThrowException(string culture)
        {
            var c = new CultureStringMapping();
            IDictionary<string, string> m = c;
            Assert.ThrowsException<CultureNotFoundException>(() => c.Add(culture, "a"));
            Assert.ThrowsException<CultureNotFoundException>(() => c[culture] = "a");
            Assert.ThrowsException<CultureNotFoundException>(() => m.Add(culture, "a"));
            Assert.ThrowsException<CultureNotFoundException>(() => m[culture] = "a");
        }
        [TestMethod]
        [DataRow("zh-cn")]
        [DataRow("zh-CN")]
        [DataRow("en-us")]
        [DataRow("en-US")]
        public void AddCultureExists_MustAdded(string culture)
        {
            var c = new CultureStringMapping();
            IDictionary<string, string> m = c;
            c.Add(culture, "hello");
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("hello", c[culture]);
            Assert.AreEqual("hello", m[culture]);

            c = new CultureStringMapping();
            c[culture] = "hello";
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual("hello", c[culture]);
            Assert.AreEqual("hello", m[culture]);
        }
        [TestMethod]
        public void CaseAdd_KeyMustSame()
        {
            var map = new CultureStringMapping();

            map.Add("zh-cn", "a");

            map["zh-CN"] = "b";
            Assert.AreEqual(1, map.Count);
            Assert.AreEqual("b", map["zh-cn"]);
            Assert.AreEqual("b", map["zh-CN"]);
        }
    }
}