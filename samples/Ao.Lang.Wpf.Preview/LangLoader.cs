using Ao.Lang.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.IO;

namespace Ao.Lang.Wpf.Preview
{
    internal class LanguageLoader : FileLanguageLoaderBase
    {
        protected override void CoreLoadCulture(ILanguageService langSer, ILanguageNode root, FileInfo input)
        {
            if (string.Equals(input.Extension,".json", StringComparison.OrdinalIgnoreCase))
            {
                root.AddJsonFile(input.FullName);
            }
        }
    }
    internal class LangLoader
    {
        public static void Load()
        {
            LanguageManager.Instance.SetCulture("zh-CN");
            var ser = LanguageManager.Instance.LangService;
            ser.AddFolder(new DirectoryInfo(GetStringFile()), new LanguageLoader());
        }

        private static string GetStringFile(params string[] nodes)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Strings");
            return Path.Combine(path, Path.Combine(nodes));
        }
    }
}
