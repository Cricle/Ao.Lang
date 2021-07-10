using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Ao.Lang
{
    public class LanguageRoot : ConfigurationRoot, ILanguageRoot
    {
        public LanguageRoot(CultureInfo culture, IList<IConfigurationProvider> providers)
            : base(providers)
        {
            Culture = culture ?? throw new ArgumentNullException(nameof(culture));
        }

        public CultureInfo Culture { get; }

        public string this[string key, params object[] args]
        {
            get
            {
                var template = base[key];
                if (string.IsNullOrEmpty(template))
                {
                    return null;
                }
                if (args is null || args.Length == 0)
                {
                    return template;
                }
                return string.Format(template, args);
            }
        }
    }
}
