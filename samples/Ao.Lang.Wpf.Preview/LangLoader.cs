using Ao.Lang.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Ao.Lang.Wpf.Preview
{
    internal class LangLoader
    {
        public static void Load()
        {
            LanguageManager.Instance.SetCulture("zh-CN");
            var ser = LanguageManager.Instance.LangService;
            var zhCNNode = ser.EnsureGetLangNode("zh-CN");
            zhCNNode.AddJsonFile(GetStringFile("zh_CN", "main.json"));
            var asm = typeof(LangLoader).Assembly;
            zhCNNode.AddResourceStream(typeof(LangLoader).Assembly.GetManifestResourceStream("Ao.Lang.Wpf.Preview.Strings.zh_CN.main.resources"));
            var enUSNode = ser.EnsureGetLangNode("en-US");
            enUSNode.AddJsonFile(GetStringFile("en_US", "main.json"));
        }

        private static string GetStringFile(params string[] nodes)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Strings");
            return Path.Combine(path, Path.Combine(nodes));
        }
    }
}
