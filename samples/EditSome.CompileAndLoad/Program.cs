using Ao.Lang.Generator;
using Ao.Lang.Generator.Editor;
using System;
using System.Globalization;
using System.IO;

namespace EditSome.CompileAndLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            var folder = new DirectoryInfo(Environment.CurrentDirectory);

            var langMgr = new LangManager(folder);
            var editor = langMgr.CreateJsonLanguageEditor<LangBlock>();
            var scope = editor.GetScope("hello");

            scope.LangBlocks.Add(new LangBlock
            {
                A = "a",
                CultureStringMapping =
                {
                    ["zh-cn"]="啊",
                    ["en-us"]="ah"
                }
            });
            scope.Compile(LangIdentityCompiler.Default);

            var langSer = editor.ToLanguageService();

            var zhNode = langSer.GetLangNode(new CultureInfo("zh-cn"));
            var enNode = langSer.GetLangNode(new CultureInfo("en-us"));

            var cn = zhNode.Root["a"];
            var en = enNode.Root["a"];
            Console.WriteLine("zh-cn=>" + cn);
            Console.WriteLine("en-us=>" + en);
        }
    }
}
