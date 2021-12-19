using Ao.Lang.Generator.Editor;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Ao.Lang.Generator.Test.Editor
{
    [TestClass]
    public class PhysicalLanguageEditorTest
    {
        class NullPhysicalLanguageEditor : PhysicalLanguageEditor<LangBlock>
        {
            public NullPhysicalLanguageEditor(DirectoryInfo workspace) : base(workspace)
            {
            }

            public override string DesignFileNameTemplate { get; } = "{0}";

            public override string CompiledFileNameTemplate { get; } = "{0}{1}";

            public override string DesignFileExtensions { get; } = "cd";

            public override string CompiledFileExtensions { get; } = "c";
            public bool IsAddCore { get; set; }
            protected override void AddCore(IConfigurationBuilder configurationBuilder, string filePath, bool optional, bool reloadOnChanged)
            {
                IsAddCore = true;
            }
            public bool IsCreateScope { get; set; }
            protected override ILanguageScope<LangBlock> CreateScope(FileInfo file, string identity)
            {
                IsCreateScope = true;
                return null;
            }
        }
        private NullPhysicalLanguageEditor CreateEditor()
        {
            return new NullPhysicalLanguageEditor(new DirectoryInfo(Environment.CurrentDirectory));
        }
        [TestMethod]
        public void GivenNullInitOrCall_MustThrowException()
        {
            var edit = CreateEditor();
            var builder = new ConfigurationBuilder();

            Assert.ThrowsException<ArgumentNullException>(() => new NullPhysicalLanguageEditor(null));
            Assert.ThrowsException<ArgumentNullException>(() => edit.Add(null, "a", false, false));
            Assert.ThrowsException<ArgumentException>(() => edit.Add(builder, null, false, false));
            Assert.ThrowsException<ArgumentNullException>(() => edit.GetCompiledFile(null, "zh-cn"));
            Assert.ThrowsException<ArgumentException>(() => edit.GetCompiledFile("a", null));
            Assert.ThrowsException<ArgumentNullException>(() => edit.GetScope(null));
        }
        [TestMethod]
        public void GetCompiledFileWhenNotExists_MustThrowException()
        {
            var edit = CreateEditor();
            Assert.ThrowsException<CultureNotFoundException>(() => edit.GetCompiledFile("a", "dsa"));
        }
        [TestMethod]
        public void GetCompiledFile_MustReturnFileInfo()
        {
            var edit = CreateEditor();
            var fi = edit.GetCompiledFile("a", "zh-cn");
            Assert.IsNotNull(fi);
        }
        [TestMethod]
        public void EnumerableFiles_MustSearched()
        {
            var edit = CreateEditor();
            edit.EnumerateCompiledFiles().ToArray();
            edit.EnumerateDesignFiles().ToArray();
        }
        [TestMethod]
        public void AddToConfiguration_WhenExtensionsNotEqual_Throw_NotItCanAdd()
        {
            var edit = CreateEditor();
            var builder = new ConfigurationBuilder();
            edit.Add(builder, "c:/a.c", false, false);
            Assert.IsTrue(edit.IsAddCore);

            edit = CreateEditor();
            Assert.ThrowsException<InvalidOperationException>(() => edit.Add(builder, "c:/a.json", false, false));
        }
        [TestMethod]
        public void CreateScope()
        {
            var edit = CreateEditor();
            edit.GetScope("a");
            Assert.IsTrue(edit.IsCreateScope);
        }
    }
}
