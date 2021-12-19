using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Ao.Lang.Test
{
    [TestClass]
    public class LanguageServiceExtensionsTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            var langSer = LanguageService.Default;
            var cultureStr = "zh-cn";
            var culture = new CultureInfo(cultureStr);

            Assert.ThrowsException<ArgumentNullException>(() => LanguageServiceExtensions.Add(null, culture, new IConfigurationSource[0]));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageServiceExtensions.Add<IConfigurationSource>(langSer, culture, null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageServiceExtensions.Add(langSer, (string)null, new IConfigurationSource[0]));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageServiceExtensions.Add(langSer, (CultureInfo)null, new IConfigurationSource[0]));

            Assert.ThrowsException<ArgumentException>(() => LanguageServiceExtensions.GetRoot(langSer, null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageServiceExtensions.GetRoot(null, cultureStr));

            Assert.ThrowsException<ArgumentNullException>(() => LanguageServiceExtensions.GetCurrentRoot(null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageServiceExtensions.GetCurrentValue(null, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageServiceExtensions.GetCurrentValue(langSer, null));

            Assert.ThrowsException<ArgumentException>(() => LanguageServiceExtensions.CultureIsSupport(langSer, null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageServiceExtensions.CultureIsSupport(null, cultureStr));

            Assert.ThrowsException<ArgumentNullException>(() => LanguageServiceExtensions.AddFromCurrentCulture(null, new IConfigurationSource[0]));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageServiceExtensions.AddFromCurrentCulture<IConfigurationSource>(langSer, null));
        }
        private LanguageService MakeService()
        {
            var culture = new CultureInfo("zh-cn");
            var ser = new LanguageService();
            var meta = new DefaultLanguageMetadata(culture);
            var mem = new MemoryConfigurationSource
            {
                InitialData = new Dictionary<string, string>
                {
                    ["Title"] = "title",
                    ["Name"] = "name"
                }
            };
            meta.Add(mem);
            ser.Add(meta);
            return ser;
        }
        [TestMethod]
        public void GetRoot()
        {
            var langSer = MakeService();
            var root = LanguageServiceExtensions.GetRoot(langSer, "zh-cn");
            Assert.IsNotNull(root);
        }
        [TestMethod]
        public void GetCurrentRoot()
        {
            var langSer = MakeService();
#if NET452
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-cn");
#else
            CultureInfo.CurrentCulture = new CultureInfo("zh-cn");
#endif
            var root = LanguageServiceExtensions.GetCurrentRoot(langSer);
            Assert.IsNotNull(root);
        }
        [TestMethod]
        public void GetCurrentValue()
        {
            var langSer = MakeService();
#if NET452
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-cn");
#else
            CultureInfo.CurrentCulture = new CultureInfo("zh-cn");
#endif
            var value = LanguageServiceExtensions.GetCurrentValue(langSer, "Title");
            Assert.AreEqual("title", value);
        }
        [TestMethod]
        public void CultureIsSupport()
        {
            var langSer = MakeService();
            var value = LanguageServiceExtensions.CultureIsSupport(langSer, "zh-cn");
            Assert.IsTrue(value);
        }
        [TestMethod]
        public void Add()
        {
            var langSer = MakeService();
            langSer.Add("en-us", new IConfigurationSource[]
            {

            });
            Assert.IsTrue(langSer.CultureIsSupport(new CultureInfo("en-us")));

            langSer.Add(new CultureInfo("fr"), new IConfigurationSource[]
            {

            });
            Assert.IsTrue(langSer.CultureIsSupport(new CultureInfo("fr")));

#if NET452
            Thread.CurrentThread.CurrentCulture = new CultureInfo("jp");
#else
            CultureInfo.CurrentCulture = new CultureInfo("jp");
#endif

            langSer.AddFromCurrentCulture(new IConfigurationSource[]
            {

            });
            Assert.IsTrue(langSer.CultureIsSupport(new CultureInfo("jp")));
        }
    }
}
