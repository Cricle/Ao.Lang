using System;
using System.IO;

namespace Ao.Lang.Runtime
{
    public abstract class FileLanguageLoaderBase : LanguageLoaderBase<FileInfo>, IFileLanguageLoader
    {
        protected override void CoreLoadCulture(ILanguageService langSer, ILanguageNode root, FileInfo input)
        {
            if (".json".Equals(input.Extension, StringComparison.OrdinalIgnoreCase))
            {
                OnJson(langSer, root, input);
            }
            else if (".xml".Equals(input.Extension, StringComparison.OrdinalIgnoreCase))
            {
                OnXml(langSer, root, input);
            }
            else if (".ini".Equals(input.Extension, StringComparison.OrdinalIgnoreCase))
            {
                OnIni(langSer, root, input);

            }
            else if (".yaml".Equals(input.Extension, StringComparison.OrdinalIgnoreCase)||
                ".yml".Equals(input.Extension, StringComparison.OrdinalIgnoreCase))
            {
                OnYaml(langSer, root, input);
            }
        }

        protected virtual void OnJson(ILanguageService langSer, ILanguageNode root, FileInfo input)
        {

        }
        protected virtual void OnXml(ILanguageService langSer, ILanguageNode root, FileInfo input)
        {

        }
        protected virtual void OnIni(ILanguageService langSer, ILanguageNode root, FileInfo input)
        {

        }
        protected virtual void OnYaml(ILanguageService langSer, ILanguageNode root, FileInfo input)
        {

        }
    }
}
