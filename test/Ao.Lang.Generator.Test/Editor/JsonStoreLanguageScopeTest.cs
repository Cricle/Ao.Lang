using Ao.Lang.Generator.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Ao.Lang.Generator.Test.Editor
{
    [TestClass]
    public class JsonStoreLanguageScopeTest
    {
        class NullJsonStoreLanguageScope : JsonStoreLanguageScope<LangBlock>
        {
            public NullJsonStoreLanguageScope(FileInfo physicalFile) : base(physicalFile)
            {
            }
            protected override void CompileItem(ILangIdentityCompiler compiler, CultureInfo culture, Dictionary<string, string> datas)
            {
            }
        }
        [TestMethod]
        public void SaveAndLoad_MustPass()
        {
            var scope = new NullJsonStoreLanguageScope(new FileInfo("a.json"));
            Assert.IsFalse(scope.Save());
            scope.LangBlocks = DataContains.CreateLangBlocks(4);
            Assert.IsTrue(scope.Save());
            scope = new NullJsonStoreLanguageScope(new FileInfo("a.json"));
            Assert.IsNotNull(scope.LangBlocks);
        }
    }
}
