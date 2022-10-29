using Ao.Lang.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.IO;

namespace Ao.Lang.Wpf.Preview
{
    internal class LangLoader : FileLanguageLoaderBase
    {
        public static void Load()
        {
            LanguageManager.Instance.SetCulture("zh-CN");
            var ser = LanguageManager.Instance.LangService;
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Strings");
            ser.AddFolder(new DirectoryInfo(path), new LangLoader());
        }

        protected override void CoreLoadCulture(ILanguageService langSer, ILanguageNode root, FileInfo input)
        {
            if (string.Equals(input.Extension, ".json", StringComparison.OrdinalIgnoreCase))
            {
                root.AddJsonFile(input.FullName);
            }
        }
    }
}
