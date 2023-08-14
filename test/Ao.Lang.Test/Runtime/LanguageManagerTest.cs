using Microsoft.Extensions.Configuration.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Ao.Lang.Runtime.Test
{
    [TestClass]
    public class LanguageManagerTest
    {
        [TestMethod]
        public void FirstGetMustNotNull()
        {
            var c = LanguageManager.Instance.CultureInfo;
            var ser = LanguageManager.Instance.LangService;
            Assert.IsNotNull(c);
            Assert.IsNotNull(ser);
        }

        [TestMethod]
        public void SwitchNull_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => LanguageManager.Instance.CultureInfo = null);
            Assert.ThrowsException<ArgumentNullException>(() => LanguageManager.Instance.LangService = null);
        }

        [TestMethod]
        public void Switch_PropertyMustSwitched()
        {
            var langSer = new LanguageService();
            var culture = new CultureInfo("fr");
            LanguageManager.Instance.LangService = langSer;
            Assert.AreEqual(langSer, LanguageManager.Instance.LangService);
            LanguageManager.Instance.CultureInfo = culture;
            Assert.AreEqual(culture, LanguageManager.Instance.CultureInfo);
        }

        [TestMethod]
        public void GetRoot_MustReturnLangSerRoot()
        {
            var mgr = LanguageManager.Instance;
            var c = new CultureInfo("zh-cn");
            mgr.CultureInfo = c;
            var node = mgr.LangService.EnsureGetLangNode(c);
            Assert.AreEqual(node.Root, mgr.Root);
        }

        [TestMethod]
        public void SwitchCulture_CultureInfoChangedMustBeRaise()
        {
            var mgr = LanguageManager.Instance;
            CultureInfo c = null;
            mgr.CultureInfoChanged += o => c = o;
            var setc = new CultureInfo("fr");
            mgr.CultureInfo = setc;
            Assert.AreEqual(setc, c);
        }

        [TestMethod]
        public void DefaultCultureInfo_MustReturnValueFromDefaultCulture()
        {
            var mgr = LanguageManager.Instance;
            mgr.DefaultCultureInfo = new CultureInfo("fr");
            mgr.CultureInfo = new CultureInfo("zh-TW");

            var defaultMap = new Dictionary<string, string>
            {
                ["Title"] = "title",
                ["Name"] = "name"
            };

            var defaultMemSource = new MemoryConfigurationSource
            {
                InitialData = defaultMap
            };
            mgr.LangService.EnsureGetLangNode(mgr.DefaultCultureInfo).Add(defaultMemSource);

            var memMap = new Dictionary<string, string>
            {
                ["Title"] = "標題",
            };

            var memSource = new MemoryConfigurationSource
            {
                InitialData = memMap
            };
            mgr.LangService.EnsureGetLangNode(mgr.CultureInfo).Add(memSource);

            Assert.AreEqual(mgr.CreateLangBox("Title").Value, memMap["Title"]);
            Assert.AreEqual(mgr.CreateLangBox("Name").Value, defaultMap["Name"]);
        }
    }
}