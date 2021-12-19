using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Ao.Lang.Generator.Test
{
    [TestClass]
    public class LangManagerTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new LangManager(null));
        }
        [TestMethod]
        public void GivenFolder_PropertyMustEqualInput()
        {
            var dir = new DirectoryInfo(Environment.CurrentDirectory);
            var langMgr = new LangManager(dir);
            Assert.AreEqual(dir, langMgr.Workspace);
        }
    }
}
