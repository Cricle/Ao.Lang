using Ao.Lang.Generator.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Resources;

namespace Ao.Lang.Generator.Test.Editor
{
    [TestClass]
    public class ResxLanguageEditorTest
    {
        [TestMethod]
        public void Compile_FileMustResources()
        {
            var editor = new ResxLanguageEditor<LangBlock>(new DirectoryInfo(Environment.CurrentDirectory));
            var scope = editor.GetScope("hello");

            scope.LangBlocks = DataContains.CreateLangBlocks(10);

            scope.Compile(LangIdentityCompiler.Default);
            var fi = editor.GetCompiledFile("hello", "zh-cn");
            using (var resx = new ResourceReader(fi.FullName))
            {
                var enu = resx.GetEnumerator();
                while (enu.MoveNext())
                {

                }
            }
        }
    }
}
