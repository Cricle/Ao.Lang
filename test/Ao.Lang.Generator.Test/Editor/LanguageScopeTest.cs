using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ao.Lang.Generator.Editor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Lang.Generator.Test.Editor
{
    [TestClass]
    public class LanguageScopeTest
    {
        class ValueLanguageScope : LanguageScope<LangBlock>
        {
            public ValueLanguageScope(FileInfo physicalFile) : base(physicalFile)
            {
            }

            protected override void CompileItem(ILangIdentityCompiler compiler, CultureInfo culture, Dictionary<string, string> datas)
            {
            }
            public IList<LangBlock> Blocks { get; set; }
            protected override IList<LangBlock> LoadFromStream(Stream stream)
            {
                return Blocks;
            }

            protected override bool SaveToStream(Stream stream, IList<LangBlock> langIdentities)
            {
                return true;
            }
        }
        [TestMethod]
        public void GivenNullInitOrCall_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ValueLanguageScope(null));
            Assert.ThrowsException<ArgumentNullException>(() => new ValueLanguageScope(new FileInfo("a.json")).LangBlocks = null);
        }
        [TestMethod]
        public void GivenFileInit_PhysicalDataMustEqualFile()
        {
            var datas = DataContains.CreateLangBlocks(5);
            var file = new FileInfo("a.json");
            var scope = new ValueLanguageScope(file)
            {
                Blocks = datas
            };
            Assert.AreEqual(file, scope.PhysicalFile);
            scope.LangBlocks = datas;
            var d = scope.LangBlocks;
            Assert.AreEqual(datas, d);
            Assert.IsTrue(scope.Save());

            file.Delete();
            scope = new ValueLanguageScope(file) { Blocks = datas };
            var blocks = scope.LangBlocks;
            Assert.AreEqual(0, blocks.Count);
        }
    }
}
