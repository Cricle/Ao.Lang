using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ao.Lang.Lookup
{
    public class LangLookup : List<ILangLookupAdder>, ILangLookup
    {
        public static readonly LangLookup Default = new LangLookup(LanguageService.Default);

        public LangLookup(ILanguageService langService)
        {
            LangService = langService ?? throw new ArgumentNullException(nameof(langService));
        }

        public ILanguageService LangService { get; }
    }
}
