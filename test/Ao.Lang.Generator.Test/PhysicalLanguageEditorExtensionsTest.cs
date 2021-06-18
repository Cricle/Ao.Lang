using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ao.Lang.Generator;
using Microsoft.Extensions.Configuration;
using System.IO;
using Ao.Lang;
using Ao.Lang.Generator.Test.Editor;
using Ao.Lang.Generator.Editor;

namespace Ao.Lang.Generator.Test
{
    [TestClass]
    public class PhysicalLanguageEditorExtensionsTest
    {
        class NullPhysicalLanguageEditor : PhysicalLanguageEditor<LangBlock>
        {
            public NullPhysicalLanguageEditor(DirectoryInfo workspace)
                : base(workspace)
            {
            }

            public override string DesignFileNameTemplate { get; }

            public override string CompiledFileNameTemplate { get; }

            public override string DesignFileExtensions { get; }

            public override string CompiledFileExtensions { get; }

            protected override void AddCore(IConfigurationBuilder configurationBuilder, string filePath, bool optional, bool reloadOnChanged)
            {
            }

            protected override ILanguageScope<LangBlock> CreateScope(FileInfo file, string identity)
            {
                return null;
            }
        }
        [TestMethod]
        public void GivenNullCall_MustThrowExcepton()
        {
            var editor = new NullPhysicalLanguageEditor(new DirectoryInfo(Environment.CurrentDirectory));
            var langSer = new LanguageService();
            var configBuilder = new ConfigurationBuilder();
            Assert.ThrowsException<ArgumentNullException>(() => PhysicalLanguageEditorExtensions.FindAndAll<LangBlock>(null, langSer));
            Assert.ThrowsException<ArgumentNullException>(() => PhysicalLanguageEditorExtensions.FindAndAll<LangBlock>(null, configBuilder));
            Assert.ThrowsException<ArgumentNullException>(() => PhysicalLanguageEditorExtensions.FindAndAll<LangBlock>(editor, (ILanguageService)null));
            Assert.ThrowsException<ArgumentNullException>(() => PhysicalLanguageEditorExtensions.FindAndAll<LangBlock>(editor, (IConfigurationBuilder)null));

            Assert.ThrowsException<ArgumentNullException>(() => PhysicalLanguageEditorExtensions.FindAndAll<LangBlock>(null, langSer, false, false));
            Assert.ThrowsException<ArgumentNullException>(() => PhysicalLanguageEditorExtensions.FindAndAll<LangBlock>(null, configBuilder, false, false));
            Assert.ThrowsException<ArgumentNullException>(() => PhysicalLanguageEditorExtensions.FindAndAll<LangBlock>(editor, (ILanguageService)null, false, false));
            Assert.ThrowsException<ArgumentNullException>(() => PhysicalLanguageEditorExtensions.FindAndAll<LangBlock>(editor, (IConfigurationBuilder)null, false, false));

            Assert.ThrowsException<ArgumentNullException>(() => PhysicalLanguageEditorExtensions.ToLanguageService<LangBlock>(null));
            Assert.ThrowsException<ArgumentNullException>(() => PhysicalLanguageEditorExtensions.ToLanguageService<LangBlock>(null,false,false));
        }
        [TestMethod]
        public void GivenAnyFiles_FindAndAdd_MustAdded()
        {
            var dir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory,"a"));
            if (dir.Exists)
            {
                dir.Delete(true);
            }
            else
            {
                dir.Create();
            }
            var jsonEditor = new JsonLanguageEditor<LangBlock>(dir);
            for (int i = 0; i < 5; i++)
            {
                var scope = jsonEditor.GetScope(i.ToString());
                scope.LangBlocks = DataContains.CreateLangBlocks(5);
                scope.Compile(LangIdentityCompiler.Default);
            }
            var langSer = new LanguageService();
            var fi=jsonEditor.FindAndAll(langSer);
            Assert.AreEqual(5 * 2, fi.Length);

            fi = jsonEditor.FindAndAll(langSer, true, false);
            Assert.AreEqual(5 * 2, fi.Length);

            var builder = new ConfigurationBuilder();
            fi = jsonEditor.FindAndAll(builder);
            Assert.AreEqual(5 * 2, fi.Length);

            fi = jsonEditor.FindAndAll(builder,true,false);
            Assert.AreEqual(5 * 2, fi.Length);

            var ser = jsonEditor.ToLanguageService();
            Assert.IsNotNull(ser);

            ser = jsonEditor.ToLanguageService(false,false);
            Assert.IsNotNull(ser);

            dir.Delete(true);
        }
    }
}
