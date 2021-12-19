using Ao.Lang.Lookup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.Lang.Test.Lookup
{
    [TestClass]
    public class DelegateLangLookupAdderTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DelegateLangLookupAdder(null));
        }
        [TestMethod]
        public void GivenDelegateInit_PropertyValueMustEqualInput()
        {
            AddLang addLang = (o, e) => false;
            var adder = new DelegateLangLookupAdder(addLang);
            Assert.AreEqual(addLang, adder.AddLang);
        }
        [TestMethod]
        public void GivenDelegateInit_Invoke_DelegateMustInvoked()
        {
            var a = false;
            AddLang addLang = (o, e) => a = true;
            var adder = new DelegateLangLookupAdder(addLang);
            adder.Add(null, null);
            Assert.IsTrue(a);
        }
    }
}
