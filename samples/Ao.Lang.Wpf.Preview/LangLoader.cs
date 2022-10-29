using Ao.Lang.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Ao.Lang.Wpf.Preview
{
    internal class LangLoader : FileLanguageLoaderBase
    {
        public static void Load()
        {
            LanguageManager.Instance.SetCulture("zh-CN");
            var ser = LanguageManager.Instance.LangService;
            ser.AddDefaultFolder(new LangLoader());
        }

        protected override void CoreLoadCulture(ILanguageService langSer, ILanguageNode root, FileInfo input)
        {
            if (".json".Equals(input.Extension, StringComparison.OrdinalIgnoreCase))
            {
                root.AddJsonFile(input.FullName);
            }
        }
    }
}
