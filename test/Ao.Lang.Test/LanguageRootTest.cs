using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ao.Lang.Test
{
    [TestClass]
    public class LanguageRootTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new LanguageRoot(null, new IConfigurationProvider[0]));
        }
        [TestMethod]
        public void InitWithCultureInfo_PropertyValueMustEquals()
        {
            var culture = new CultureInfo("zh-cn");
            var root = new LanguageRoot(culture, new IConfigurationProvider[0]);
            Assert.AreEqual(culture, root.Culture);
        }
        [TestMethod]
        public void ThisGet_MustGotString()
        {
            var culture = new CultureInfo("zh-cn");
            var source = new MemoryConfigurationSource
            {
                InitialData = new Dictionary<string, string>
                {
                    ["Title"] = "title",
                    ["Format"] = "hello{0}!"
                }
            };
            var provider = source.Build(new ConfigurationBuilder());
            var root = new LanguageRoot(culture, new IConfigurationProvider[] { provider });
            var title = root["Title"];
            Assert.AreEqual("title", title);
            var formatFail = root["Format", null];
            Assert.AreEqual("hello{0}!", formatFail);
            var formatSucceed = root["Format", "a"];
            Assert.AreEqual("helloa!", formatSucceed);
        }
    }
}
