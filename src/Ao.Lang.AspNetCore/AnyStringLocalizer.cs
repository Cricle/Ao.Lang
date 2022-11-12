using Ao.Lang;
using Ao.Lang.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public class AnyStringLocalizer<T> : AnyStringLocalizer
    {
        public AnyStringLocalizer(ILangSectionProvider provider, ILanguageRoot root) 
            : base(provider.GetSectionKey(typeof(T)), root)
        {
        }
    }
    public class AnyStringLocalizer : IStringLocalizer
    {
        public AnyStringLocalizer(string sectionKey, ILanguageRoot root)
        {
            SectionKey = sectionKey;
            Root = root;
        }

        public string SectionKey { get; }

        public ILanguageRoot Root { get; }

        public LocalizedString this[string name]
        {
            get
            {
                var s = GetString(name);
                return new LocalizedString(name, s??string.Empty, s == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var s = GetString(name);
                if (s != null && arguments != null)
                {
                    s = string.Format(s, arguments);
                }
                return new LocalizedString(name, s ?? string.Empty, s == null);
            }
        }
        protected string GetString(string name)
        {
            if (string.IsNullOrEmpty(SectionKey))
            {
                return Root[name];
            }
            return Root[ConfigurationPath.Combine(SectionKey, name)];
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            IEnumerable<KeyValuePair<string, string>> section;
            if (string.IsNullOrEmpty(SectionKey))
            {
                section = Root.AsEnumerable();
            }
            else
            {
                section = Root.GetSection(SectionKey).AsEnumerable();
            }

            foreach (var item in section)
            {
                yield return new LocalizedString(item.Key, item.Value);
            }
        }
    }
}
