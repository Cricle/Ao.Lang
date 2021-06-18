using Ao.Lang.Generator;
using Ao.Lang.Generator.Editor;
using System;
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
            
        }
    }
}
