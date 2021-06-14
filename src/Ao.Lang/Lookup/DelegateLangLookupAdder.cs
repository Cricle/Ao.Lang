using System;
using System.Diagnostics;

namespace Ao.Lang.Lookup
{
    public delegate bool AddLang(ILanguageService langSer, LangLookupBox box);
    public class DelegateLangLookupAdder : ILangLookupAdder
    {
        public DelegateLangLookupAdder(AddLang addLang)
        {
            AddLang = addLang ?? throw new ArgumentNullException(nameof(addLang));
        }

        public AddLang AddLang { get; }

        public bool Add(ILanguageService langSer, LangLookupBox box)
        {
            Debug.Assert(AddLang != null);
            return AddLang(langSer, box);
        }
    }
}
