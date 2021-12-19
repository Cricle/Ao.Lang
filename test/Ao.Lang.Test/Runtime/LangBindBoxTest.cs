using Ao.Lang.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace Ao.Lang.Test.Runtime
{
    [TestClass]
    public class LangBindBoxTest
    {
        class Box2
        {

        }
        class Box
        {
            public string A { get; set; }
            public int B { get; set; }

            public static readonly PropertyInfo AProperty = typeof(Box).GetProperty(nameof(A));
            public static readonly PropertyInfo BProperty = typeof(Box).GetProperty(nameof(B));
        }
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            var prop = Box.AProperty;
            var inst = new Box();

            Assert.ThrowsException<ArgumentNullException>(() => new LangBindBox(prop, null));
            Assert.ThrowsException<ArgumentNullException>(() => new LangBindBox(null, inst));
        }
        [TestMethod]
        public void GivenNotStringTypeProperty_MustThrowException()
        {
            var prop = Box.BProperty;
            var inst = new Box();

            Assert.ThrowsException<ArgumentException>(() => new LangBindBox(prop, inst));
        }
        [TestMethod]
        public void GivenNotInstanceOf_MustThrowException()
        {
            var prop = Box.BProperty;
            var inst = new Box2();

            Assert.ThrowsException<ArgumentException>(() => new LangBindBox(prop, inst));
        }
        [TestMethod]
        public void RaiseChanged()
        {
            var prop = Box.AProperty;
            var inst = new Box();

            var box = new LangBindBox(prop, inst);
            box.ReceivedChanged(null, new LangValueChangeEventArgs(null, "Aaa"));

            Assert.AreEqual("Aaa", inst.A);

            box = new LangBindBox(prop, inst);
            box.ReceivedChanged(null, new LangValueChangeEventArgs(null, "BBB"));

            Assert.AreEqual("BBB", inst.A);
        }
    }
}
