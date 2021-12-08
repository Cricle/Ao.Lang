using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

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
            var node=mgr.LangService.EnsureGetLangNode(c);
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
    }
}
