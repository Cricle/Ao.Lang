using Ao.Lang.Lookup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.Lang.Test.Lookup
{
    [TestClass]
    public class LangLookupTest
    {
        [TestMethod]
        public void GetTwiceDefault_MustEqual()
        {
            var a = LangLookup.Default;
            var b = LangLookup.Default;
            Assert.AreEqual(a, b);
        }
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new LangLookup(null));
        }
        [TestMethod]
        public void GivenLangSerInit_PropertyMustEqualInput()
        {
            var langSer = new LanguageService();
            var lookup = new LangLookup(langSer);
            Assert.AreEqual(langSer, lookup.LangService);
        }
    }
}
