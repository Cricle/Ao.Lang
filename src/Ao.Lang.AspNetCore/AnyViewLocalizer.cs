using Ao.Lang;
using Ao.Lang.Runtime;
using Microsoft.AspNetCore.Mvc.Localization;

namespace Microsoft.Extensions.DependencyInjection
{
    public class AnyViewLocalizer<T> : AnyViewLocalizer
    {
        public AnyViewLocalizer(ILangSectionProvider provider, ILanguageRoot root)
            : base(provider.GetSectionKey(typeof(T)), root)
        {
        }
    }
    public class AnyViewLocalizer : AnyHtmlLocalizer, IViewLocalizer
    {
        public AnyViewLocalizer(string sectionKey, ILanguageRoot root) : base(sectionKey, root)
        {
        }
    }
}
