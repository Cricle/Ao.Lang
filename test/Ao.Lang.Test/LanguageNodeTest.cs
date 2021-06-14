using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System.Threading;

namespace Ao.Lang.Test
{
    [TestClass]
    public class LanguageNodeTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new LanguageNode(null));
        }
        [TestMethod]
        public void BuildUseConfigurationBuilder_MustThrowException()
        {
            var node = new LanguageNode(new CultureInfo("zh-cn"));
            Assert.ThrowsException<NotSupportedException>(() => ((IConfigurationBuilder)node).Build());
        }
        [TestMethod]
        public void WhenNoRebuildOnChange_ChaneWasNotRebuilt()
        {
            var culture = new CultureInfo("en-us");
#if NET452
            Thread.CurrentThread.CurrentCulture = culture;
#else
            CultureInfo.CurrentCulture = culture;
#endif
            var node = new LanguageNode(culture);
            node.ReBuildIfCollectionChanged = false;
            var root = node.Root;
            var memSource = new MemoryConfigurationSource
            {
                InitialData = new Dictionary<string, string>
                {
                    ["Title"] = "title",
                    ["Name"] = "name"
                }
            };
            node.Add(memSource);
            var root2 = node.Root;
            Assert.AreEqual(root, root2);
            var val = root2["Title"];
            Assert.IsNull(val);
            node.ReBuild();

            var root3 = node.Root;
            Assert.AreNotEqual(root2, root3);
            val = root3["Title"];
            Assert.AreEqual("title",val);
        }
        [TestMethod]
        public void WhenRebuildOnChange_GetAny_MustReturnString()
        {
            var culture = new CultureInfo("en-us");
            var node = new LanguageNode(culture);
            node.ReBuildIfCollectionChanged = true;
            Assert.AreEqual(culture, node.Culture);
            Assert.IsFalse(node.IsBuilt);
            var memSource = new MemoryConfigurationSource
            {
                InitialData = new Dictionary<string, string>
                {
                    ["Title"] = "title",
                    ["Name"] = "name"
                }
            };
            node.Add(memSource);
            var root = node.Root;
            Assert.IsTrue(node.IsBuilt);
            Assert.IsNotNull(root);
            var val = root["Title"];
            Assert.AreEqual("title", val);
            node.Remove(node.First());
            Assert.IsFalse(node.IsBuilt);
            var root2 = node.Root;
            Assert.AreNotEqual(root, root2);
        }
    }
}
