using System.Globalization;

namespace Ao.Lang.Runtime
{
    public abstract class LanguageLoaderBase<TInput> : ILanguageLoader<TInput>
    {
        public void LoadCulture(ILanguageService langSer, CultureInfo culture, TInput input)
        {
            CoreLoadCulture(langSer, langSer.EnsureGetLangNode(culture), input);
        }
        protected abstract void CoreLoadCulture(ILanguageService langSer, ILanguageNode root, TInput input);
    }
}
