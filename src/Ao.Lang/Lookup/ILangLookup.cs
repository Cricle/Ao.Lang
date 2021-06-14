using System.Collections.Generic;

namespace Ao.Lang.Lookup
{
    public interface ILangLookup : IList<ILangLookupAdder>
    {
        ILanguageService LangService { get; }
    }
}
