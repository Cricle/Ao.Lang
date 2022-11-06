using Ao.Lang.Runtime;
using System.Globalization;

namespace Microsoft.Extensions.DependencyInjection
{
    public class LangOptions
    {
        public string Default { get; set; } = CultureInfo.CurrentCulture.Name;

        public string RouteKey { get; set; } = "lang";

        public string QueryKey { get; set; } = "lang";

        public bool UseAcceptLanguage { get; set; } = true;

        public bool NotFoundUseDefault { get; set; }

        public LanguageManager LanguageManager { get; set; } = LanguageManager.Instance;
    }
}
