using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Lang.Generator.Test
{
    [TestClass]
    public class DefaultLangIdentityTest
    {
        [TestMethod]
        public void GivenAnyPart_GetResultMustContainsThem()
        {
            var identity = new DefaultLangIdentity();
            identity.Add("a");
            identity.Add("b");
            identity.Add("c");

            var parts = identity.GetIdentityBlocks();

            Assert.AreEqual("a", parts[0]);
            Assert.AreEqual("b", parts[1]);
            Assert.AreEqual("c", parts[2]);

            identity = new DefaultLangIdentity(new[] { "a", "b", "c" });

            parts = identity.GetIdentityBlocks();

            Assert.AreEqual("a", parts[0]);
            Assert.AreEqual("b", parts[1]);
            Assert.AreEqual("c", parts[2]);

            identity = new DefaultLangIdentity(3);
            identity.Add("a");
            identity.Add("b");
            identity.Add("c");

            parts = identity.GetIdentityBlocks();

            Assert.AreEqual("a", parts[0]);
            Assert.AreEqual("b", parts[1]);
            Assert.AreEqual("c", parts[2]);
        }
    }
}
