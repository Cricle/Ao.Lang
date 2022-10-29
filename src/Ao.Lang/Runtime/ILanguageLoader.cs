using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Ao.Lang.Runtime
{
    public interface ILanguageLoader<TInput>
    {
        void LoadCulture(ILanguageService langSer,CultureInfo culture,TInput input);
    }
    public interface IFileLanguageLoader : ILanguageLoader<FileInfo>
    {

    }
    public abstract class FileLanguageLoaderBase: LanguageLoaderBase<FileInfo>, IFileLanguageLoader
    {

    }
    public abstract class LanguageLoaderBase<TInput> : ILanguageLoader<TInput>
    {
        public void LoadCulture(ILanguageService langSer, CultureInfo culture, TInput input)
        {
            CoreLoadCulture(langSer, langSer.EnsureGetLangNode(culture), input);
        }
        protected abstract void CoreLoadCulture(ILanguageService langSer, ILanguageNode root, TInput input);
    }
}
