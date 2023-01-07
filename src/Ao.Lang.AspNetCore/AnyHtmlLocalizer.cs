using Ao.Lang;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace Microsoft.Extensions.DependencyInjection
{
    public class AnyHtmlLocalizer<T> : AnyHtmlLocalizer, IHtmlLocalizer<T>
    {
        public AnyHtmlLocalizer(ILangSectionProvider provider, ILanguageRoot root)
            : base(provider.GetSectionKey(typeof(T)), root)
        {
        }
    }

    public class AnyHtmlLocalizer : AnyStringLocalizer, IHtmlLocalizer
    {
        public AnyHtmlLocalizer(string sectionKey, ILanguageRoot root) : base(sectionKey, root)
        {
        }

        public new LocalizedHtmlString this[string name]
        {
            get
            {
                var s = base.GetString(name);
                return new LocalizedHtmlString(name, s ?? string.Empty, s == null);
            }
        }

        public new LocalizedHtmlString this[string name, params object[] arguments]
        {
            get
            {
                var s = base.GetString(name);
                if (s != null && arguments != null)
                {
                    s = string.Format(s, arguments);
                }
                return new LocalizedHtmlString(name, s ?? string.Empty, s == null);
            }
        }

        public new LocalizedString GetString(string name)
        {
            return base[name];
        }

        public LocalizedString GetString(string name, params object[] arguments)
        {
            return base[name, arguments];
        }
    }
}