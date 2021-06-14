using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ao.Lang
{
    public class LanguageBuilder : ConfigurationBuilder, ILanguageBuilder, IConfigurationBuilder
    {
        public LanguageBuilder(CultureInfo culture)
        {
            Culture = culture ?? throw new ArgumentNullException(nameof(culture));
        }

        public CultureInfo Culture { get; }

        IConfigurationRoot IConfigurationBuilder.Build()
        {
            return Build();
        }
        public new ILanguageRoot Build()
        {
            Debug.Assert(Culture != null);
            var providers = Sources.Select(x => x.Build(this)).ToArray();
            return new LanguageRoot(Culture, providers);
        }
    }
}
