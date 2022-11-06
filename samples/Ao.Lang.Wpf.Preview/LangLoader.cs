using Ao.Lang.Runtime;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Ao.Lang.Wpf.Preview
{
    internal class LangLoader : FileLanguageLoaderBase
    {
        public static void Load(string culture=null)
        {
            LanguageManager.Instance.SetCulture(culture??"zh-cn");
            var ser = LanguageManager.Instance.LangService;
            ser.AddDefaultFolder(new LangLoader());
        }

        protected override void OnJson(ILanguageService langSer, ILanguageNode root, FileInfo input)
        {
            root.AddJsonFile(input.FullName);
        }
    }
}
