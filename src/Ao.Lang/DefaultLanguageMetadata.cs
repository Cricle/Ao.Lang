using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ao.Lang
{
    public class DefaultLanguageMetadata : List<IConfigurationSource>, ILanguageMetadata
    {
        public DefaultLanguageMetadata(CultureInfo cultureInfo)
            : this(cultureInfo, 0)
        {

        }
        public DefaultLanguageMetadata(CultureInfo cultureInfo, int capacity)
            : base(capacity)
        {
            if (cultureInfo is null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            Culture = cultureInfo;
        }

        public DefaultLanguageMetadata(CultureInfo cultureInfo, IEnumerable<IConfigurationSource> sources)
        {
            if (cultureInfo is null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            if (sources is null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            Culture = cultureInfo;
            AddRange(sources);
        }

        public virtual CultureInfo Culture { get; }
    }
}
