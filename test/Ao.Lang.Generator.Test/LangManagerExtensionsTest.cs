using Ao.Lang.Generator.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Ao.Lang.Generator.Test
{
    [TestClass]
    public class LangManagerExtensionsTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => JsonLangManagerExtensions.CreateJsonLanguageEditor<LangBlock>(null));
            Assert.ThrowsException<ArgumentNullException>(() => ResourcesLangManagerExtensions.CreateResxLanguageEditor<LangBlock>(null));
        }
        [TestMethod]
        public void GivenValueCreateEditor_MustCreated()
        {
            var mgr = new LangManager(new DirectoryInfo(Environment.CurrentDirectory));
            var jsonEditor = JsonLangManagerExtensions.CreateJsonLanguageEditor<LangBlock>(mgr);
            Assert.IsNotNull(jsonEditor);
            Assert.AreEqual(mgr.Workspace, jsonEditor.Workspace);
            var resxEditor = ResourcesLangManagerExtensions.CreateResxLanguageEditor<LangBlock>(mgr);
            Assert.IsNotNull(resxEditor);
            Assert.AreEqual(mgr.Workspace, resxEditor.Workspace);
        }
    }
}
