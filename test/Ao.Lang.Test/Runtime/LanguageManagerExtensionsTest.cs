using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Ao.Lang.Runtime.Test
{
    [TestClass]
    public class LanguageManagerExtensionsTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => LanguageManagerExtensions.IsCulture(null, "zh-cn"));
            Assert.ThrowsException<ArgumentException>(() => LanguageManagerExtensions.IsCulture(LanguageManager.Instance, null));
            
            Assert.ThrowsException<ArgumentException>(() => LanguageManagerExtensions.SetCulture(LanguageManager.Instance, null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageManagerExtensions.SetCulture(null, "zh-cn"));
            
            Assert.ThrowsException<ArgumentNullException>(() => LanguageManagerExtensions.SwitchIfNot(null, "zh-cn", "en-us"));
            Assert.ThrowsException<ArgumentException>(() => LanguageManagerExtensions.SwitchIfNot(LanguageManager.Instance, null, "en-us"));
            Assert.ThrowsException<ArgumentException>(() => LanguageManagerExtensions.SwitchIfNot(LanguageManager.Instance, null, new CultureInfo("zh-cn")));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageManagerExtensions.SwitchIfNot(null, "en-us", new CultureInfo("zh-cn")));
            Assert.ThrowsException<ArgumentException>(() => LanguageManagerExtensions.SwitchIfNot(LanguageManager.Instance, "zh-cn", (string)null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageManagerExtensions.SwitchIfNot(LanguageManager.Instance, "zh-cn", (CultureInfo)null));
        }
        [TestMethod]
        public void SwitchCulture_CultureMustBeSwitched()
        {
            var mgr = new LanguageManager();
            LanguageManagerExtensions.SetCulture(mgr, "fr");
            Assert.AreEqual("fr", mgr.CultureInfo.Name);
        }
        [TestMethod]
        public void IsCulture_MustReturnActual()
        {
            var mgr = new LanguageManager();
            var c = new CultureInfo("fr");
            mgr.CultureInfo = c;
            Assert.IsTrue(LanguageManagerExtensions.IsCulture(mgr, "fr"));
            Assert.IsFalse(LanguageManagerExtensions.IsCulture(mgr, "zh-cn"));
        }
        [TestMethod]
        public void SwitchIfNot_EqualMustDoNothing_NotEqualMustChanged()
        {
            var mgr = new LanguageManager();
            var fr = new CultureInfo("fr");
            var zhcn = new CultureInfo("zh-cn");
            mgr.CultureInfo = fr;
            LanguageManagerExtensions.SwitchIfNot(mgr, "fr", zhcn);
            Assert.AreEqual(zhcn.Name, mgr.CultureInfo.Name);
            mgr.CultureInfo = fr;
            LanguageManagerExtensions.SwitchIfNot(mgr, "fr", fr);
            Assert.AreEqual(fr.Name, mgr.CultureInfo.Name);
            mgr.CultureInfo = fr;
            LanguageManagerExtensions.SwitchIfNot(mgr, "fr", "fr");
            Assert.AreEqual(fr.Name, mgr.CultureInfo.Name);
            mgr.CultureInfo = fr;
            LanguageManagerExtensions.SwitchIfNot(mgr, "fr", "zh-cn");
            Assert.AreEqual(zhcn.Name, mgr.CultureInfo.Name);
        }
    }
}
