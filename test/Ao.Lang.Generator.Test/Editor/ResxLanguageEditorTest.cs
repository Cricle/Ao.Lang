using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ao.Lang.Generator.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

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
            var fi=editor.GetCompiledFile("hello", "zh-cn");
            using (var resx=new ResourceReader(fi.FullName))
            {
                var enu = resx.GetEnumerator();
                while (enu.MoveNext())
                {

                }
            }
        }
    }
}
