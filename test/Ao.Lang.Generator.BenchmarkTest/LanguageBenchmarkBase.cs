using Ao.Lang.Generator.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ao.Lang.Generator.BenchmarkTest
{
    public abstract class LanguageBenchmarkBase
    {
        protected readonly DirectoryInfo Folder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        protected readonly string jsonCompiledFile;
        protected readonly string resxCompiledFile;
        protected readonly IList<LangBlock> datas;

        public LanguageBenchmarkBase(int count)
        {
            datas = new List<LangBlock>();
            for (int i = 0; i < count; i++)
            {
                datas.Add(new LangBlock
                {
                    A = "d" + i,
                    B = "f" + i,
                    C= "m" + i,
                    D= "y" + i,
                    CultureStringMapping =
                    {
                        ["zh-cn"]="你好鸭"+i,
                        ["en-us"]="Hello"+i,
                    }
                });
            }
            jsonCompiledFile = Compile("testjson", new JsonLanguageEditor<LangBlock>(Folder));
            resxCompiledFile = Compile("testresx", new ResxLanguageEditor<LangBlock>(Folder));

        }
        protected string Compile(string identity, ILanguageEditor<LangBlock> editor)
        {
            var scope = editor.GetScope(identity);
            scope.LangBlocks = datas;
            scope.Save();
            scope.Compile(LangIdentityCompiler.Default);
            return editor.EnumerateCompiledFiles().First().FullName;
        }

    }
}
