using Ao.Lang.Lookup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Ao.Lang.Test.Lookup
{
    [TestClass]
    public class LangLookupExtensionsTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            AddLang func = (o, e) => false;
            var lookup = LangLookup.Default;
            Action<ILanguageNode, LangLookupBox> act = (o, e) => { };

            Assert.ThrowsException<ArgumentNullException>(() => LangLookupExtensions.Add(null, func));
            Assert.ThrowsException<ArgumentNullException>(() => LangLookupExtensions.Add(lookup, null));

            Assert.ThrowsException<ArgumentNullException>(() => LangLookupExtensions.EnableFileType(null, "json", act));
            Assert.ThrowsException<ArgumentException>(() => LangLookupExtensions.EnableFileType(lookup, null, act));
            Assert.ThrowsException<ArgumentNullException>(() => LangLookupExtensions.EnableFileType(lookup, "json", null));

            Assert.ThrowsException<ArgumentNullException>(() => LangLookupExtensions.MakeLookup(null));
            Assert.ThrowsException<ArgumentNullException>(() => LangLookupExtensions.Raise(null, "a"));
            Assert.ThrowsException<ArgumentException>(() => LangLookupExtensions.Raise(lookup, (string)null));
            Assert.ThrowsException<ArgumentNullException>(() => LangLookupExtensions.Raise(lookup, (Stream)null));
            Assert.ThrowsException<ArgumentNullException>(() => LangLookupExtensions.Raise(null, "a", false, false));
            Assert.ThrowsException<ArgumentException>(() => LangLookupExtensions.Raise(lookup, null, false, false));

            Assert.ThrowsException<ArgumentNullException>(() => LangLookupExtensions.RaiseDirectory(null, "a"));
            Assert.ThrowsException<ArgumentException>(() => LangLookupExtensions.RaiseDirectory(lookup, null));
            Assert.ThrowsException<ArgumentNullException>(() => LangLookupExtensions.RaiseDirectory(null, "a", SearchOption.AllDirectories));
            Assert.ThrowsException<ArgumentException>(() => LangLookupExtensions.RaiseDirectory(lookup, null, SearchOption.AllDirectories));
            Assert.ThrowsException<ArgumentNullException>(() => LangLookupExtensions.RaiseDirectory(null, "a", SearchOption.AllDirectories, false, false));
            Assert.ThrowsException<ArgumentException>(() => LangLookupExtensions.RaiseDirectory(lookup, null, SearchOption.AllDirectories, false, false));
        }
        [TestMethod]
        public void EnableType_LookupMustContains()
        {
            Action<ILanguageNode, LangLookupBox> act = (o, e) => { };
            var lookup = new LangLookup(new LanguageService());
            lookup.EnableFileType("qaq", act);
        }
        [TestMethod]
        public void MakeLookup_LookupLangSerMustEqualInput()
        {
            var langSer = new LanguageService();
            var lookup = LangLookupExtensions.MakeLookup(langSer);
            Assert.AreEqual(langSer, lookup.LangService);
        }
        [TestMethod]
        public void AddWithDelegate_MustContainsIt()
        {
            AddLang func = (o, e) => false;
            var lookup = new LangLookup(new LanguageService());
            LangLookupExtensions.Add(lookup, func);
            Assert.AreEqual(1, lookup.Count);
            var x = lookup.Single();
            Assert.IsInstanceOfType(x, typeof(DelegateLangLookupAdder));
            var adder = (DelegateLangLookupAdder)x;
            Assert.AreEqual(func, adder.AddLang);
        }
        [TestMethod]
        public void Nothing_Raise_MustReturnFalse()
        {
            var lookup = new LangLookup(new LanguageService());
            var ok = lookup.Raise("C:/a.zh-cn.json");
            Assert.IsFalse(ok);
        }
        [TestMethod]
        public void AddOne_Raise_MustStepOutWhenTrue()
        {
            var lookup = new LangLookup(new LanguageService());
            var okList = new bool[4];
            for (int i = 0; i < 4; i++)
            {
                var a = i;
                lookup.Add(new DelegateLangLookupAdder((_, __) =>
                {
                    okList[a] = true;
                    return a == 2;
                }));
            }
            void RaiseCheck()
            {
                Assert.IsTrue(okList[0]);
                Assert.IsTrue(okList[1]);
                Assert.IsTrue(okList[2]);
                Assert.IsFalse(okList[3]);
            }
            var ok = lookup.Raise("C:/a.zh-cn.json");
            Assert.IsTrue(ok);
            RaiseCheck();
            ok = lookup.Raise(Stream.Null);
            Assert.IsTrue(ok);
            RaiseCheck();
            ok = lookup.Raise("C:/a.zh-cn.json", false, false);
            Assert.IsTrue(ok);
            RaiseCheck();
        }
    }
}
